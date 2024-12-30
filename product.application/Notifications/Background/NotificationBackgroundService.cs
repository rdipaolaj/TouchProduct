using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace product.application.Notifications.Background;
public class NotificationBackgroundService : BackgroundService
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<NotificationBackgroundService> _logger;

    public NotificationBackgroundService(INotificationService notificationService, ILogger<NotificationBackgroundService> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("El servicio en segundo plano de notificaciones ha iniciado.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Iniciando el proceso de notificación de inventarios bajos...");

                var response = await _notificationService.NotifyLowStockProductsAsync(stoppingToken);

                if (response.Success)
                {
                    _logger.LogInformation("La notificación de inventarios bajos se envió correctamente.");
                }
                else
                {
                    _logger.LogWarning($"El proceso de notificaciones se completó, pero no devolvió resultados accionables. Mensaje: {response.Message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error durante el procesamiento de notificaciones de inventarios bajos.");
            }

            const int delayHours = 1;
            _logger.LogInformation($"El próximo proceso de notificación se ejecutará en {delayHours} hora(s).");
            await Task.Delay(TimeSpan.FromHours(delayHours), stoppingToken);
        }

        _logger.LogInformation("El servicio en segundo plano de notificaciones se ha detenido.");
    }
}