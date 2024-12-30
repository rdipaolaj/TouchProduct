
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using product.application.Email;
using product.common.Responses;
using product.data.Repository.Interfaces;
using product.internalservices.User;
using product.request.Commands.v1.Notification;

namespace product.application.Notifications;
public class NotificationService : INotificationService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(IServiceScopeFactory scopeFactory, IEmailService emailService, IUserService userService, ILogger<NotificationService> logger)
    {
        _scopeFactory = scopeFactory;
        _emailService = emailService;
        _userService = userService;
        _logger = logger;
    }

    public async Task<ApiResponse<bool>> NotifyLowStockProductsAsync(ForceNotificationsCommand request, CancellationToken cancellationToken)
    {
        return await NotifyLowStockProductsInternalAsync(cancellationToken);
    }

    public async Task<ApiResponse<bool>> NotifyLowStockProductsAsync(CancellationToken cancellationToken)
    {
        return await NotifyLowStockProductsInternalAsync(cancellationToken);
    }

    private async Task<ApiResponse<bool>> NotifyLowStockProductsInternalAsync(CancellationToken cancellationToken)
    {
        try
        {
            const int lowStockThreshold = 5; // Umbral para inventario bajo
            _logger.LogInformation("Verificando productos con inventario bajo...");

            using var scope = _scopeFactory.CreateScope();
            var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

            var lowStockProducts = await productRepository.GetLowStockProductsAsync(lowStockThreshold, cancellationToken);

            if (!lowStockProducts.Any())
            {
                _logger.LogInformation("No se encontraron productos con inventario bajo.");
                return ApiResponseHelper.CreateSuccessResponse(false, "No se encontraron productos con inventario bajo.");
            }

            _logger.LogInformation($"Se encontraron {lowStockProducts.Count()} productos con inventario bajo.");

            var adminEmailsResponse = await _userService.GetAdminEmailsAsync(cancellationToken);

            if (!adminEmailsResponse.Success || !adminEmailsResponse.Data.Any())
            {
                _logger.LogError("No hay correos electrónicos de administradores disponibles para enviar notificaciones.");
                return ApiResponseHelper.CreateErrorResponse<bool>("No hay correos electrónicos de administradores disponibles.");
            }

            var failedEmails = new List<string>();

            await Parallel.ForEachAsync(adminEmailsResponse.Data, cancellationToken, async (email, token) =>
            {
                var message = "Los siguientes productos tienen inventario bajo:\n" +
                              string.Join("\n", lowStockProducts.Select(p => $"{p.Nombre}: {p.Cantidad} unidades"));

                var emailSent = await _emailService.SendEmailAsync(email, "Notificación de Inventario Bajo", message);

                if (!emailSent)
                {
                    _logger.LogWarning($"No se pudo enviar la notificación a: {email}");
                    lock (failedEmails)
                    {
                        failedEmails.Add(email);
                    }
                }
            });

            if (failedEmails.Any())
            {
                _logger.LogWarning($"No se pudieron enviar notificaciones a los siguientes correos: {string.Join(", ", failedEmails)}");
                return ApiResponseHelper.CreateErrorResponse<bool>("Error al enviar algunas notificaciones de inventario bajo.");
            }

            _logger.LogInformation("Notificación de inventario bajo enviada a todos los administradores.");
            return ApiResponseHelper.CreateSuccessResponse(true, "Notificación de inventario bajo enviada correctamente a todos los administradores.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error en {nameof(NotifyLowStockProductsInternalAsync)}: {ex.Message}");
            return ApiResponseHelper.CreateErrorResponse<bool>("Error al procesar la notificación de inventario bajo.");
        }
    }
}
