using MediatR;
using Microsoft.Extensions.Logging;
using product.application.Notifications;
using product.common.Responses;
using product.dto.Notification.v1;
using product.request.Commands.v1.Notification;

namespace product.handler.Notification;
public class ForceNotificationsCommandHandler : IRequestHandler<ForceNotificationsCommand, ApiResponse<ForceNotificationsResponse>>
{
    private readonly ILogger<ForceNotificationsCommandHandler> _logger;
    private readonly INotificationService _notificationService;

    public ForceNotificationsCommandHandler(ILogger<ForceNotificationsCommandHandler> logger, INotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }
    public async Task<ApiResponse<ForceNotificationsResponse>> Handle(ForceNotificationsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling ForceNotificationsCommand...");
        var notificationResult = await _notificationService.NotifyLowStockProductsAsync(request, cancellationToken);

        if (!notificationResult.Success)
        {
            return ApiResponseHelper.CreateErrorResponse<ForceNotificationsResponse>(
                notificationResult.Message,
                notificationResult.StatusCode
            );
        }

        return ApiResponseHelper.CreateSuccessResponse(
            new ForceNotificationsResponse { Success = notificationResult.Data },
            notificationResult.Message
        );
    }
}
