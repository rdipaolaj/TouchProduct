using MediatR;
using product.common.Responses;
using product.dto.Notification.v1;

namespace product.request.Commands.v1.Notification;
public class ForceNotificationsCommand : IRequest<ApiResponse<ForceNotificationsResponse>>
{
}