using product.common.Responses;
using product.request.Commands.v1.Notification;

namespace product.application.Notifications;
public interface INotificationService
{
    Task<ApiResponse<bool>> NotifyLowStockProductsAsync(ForceNotificationsCommand request, CancellationToken cancellationToken);
    Task<ApiResponse<bool>> NotifyLowStockProductsAsync(CancellationToken cancellationToken);
}
