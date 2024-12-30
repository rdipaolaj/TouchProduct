using Microsoft.Extensions.Logging;
using Moq;
using product.application.Notifications;
using product.common.Responses;
using product.handler.Notification;
using product.request.Commands.v1.Notification;

namespace product.test.Handlers;

[TestFixture]
public class ForceNotificationsCommandHandlerTests
{
    private Mock<ILogger<ForceNotificationsCommandHandler>> _loggerMock;
    private Mock<INotificationService> _notificationServiceMock;
    private ForceNotificationsCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<ForceNotificationsCommandHandler>>();
        _notificationServiceMock = new Mock<INotificationService>();
        _handler = new ForceNotificationsCommandHandler(_loggerMock.Object, _notificationServiceMock.Object);
    }

    [Test]
    public async Task Handle_ReturnsSuccessResponse_WhenNotificationSucceeds()
    {
        _notificationServiceMock.Setup(s => s.NotifyLowStockProductsAsync(It.IsAny<ForceNotificationsCommand>(), default))
            .ReturnsAsync(ApiResponseHelper.CreateSuccessResponse(true, "Notifications sent successfully"));

        var result = await _handler.Handle(new ForceNotificationsCommand(), default);

        Assert.IsTrue(result.Success);
        Assert.IsTrue(result.Data.Success);
    }
}