using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using product.api.Configuration;
using product.request.Commands.v1.Notification;

namespace product.api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("touch/notifications/api/v{v:apiVersion}/[controller]")]
public class NotificationController : CustomController
{
    private readonly ILogger<NotificationController> _logger;
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator, ILogger<NotificationController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [Route("force-notifications")]
    [MapToApiVersion(1)]
    [Authorize]
    public async Task<IActionResult> ForceNotifications()
    {
        _logger.LogInformation("ForceNotifications");
        var result = await _mediator.Send(new ForceNotificationsCommand());
        return OkorBadRequestValidationApiResponse(result);
    }
}
