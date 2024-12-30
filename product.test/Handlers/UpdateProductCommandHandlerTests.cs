using Microsoft.Extensions.Logging;
using Moq;
using product.application.Product;
using product.handler.Product;
using product.request.Commands.v1.Product;

namespace product.test.Handlers;

[TestFixture]
public class UpdateProductCommandHandlerTests
{
    private Mock<ILogger<UpdateProductCommandHandler>> _loggerMock;
    private Mock<IProductService> _productServiceMock;
    private UpdateProductCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<UpdateProductCommandHandler>>();
        _productServiceMock = new Mock<IProductService>();
        _handler = new UpdateProductCommandHandler(_loggerMock.Object, _productServiceMock.Object);
    }

    [Test]
    public async Task Handle_ReturnsSuccessResponse_WhenProductUpdated()
    {
        _productServiceMock.Setup(s => s.UpdateProductAsync(It.IsAny<UpdateProductCommand>(), default)).ReturnsAsync(true);

        var command = new UpdateProductCommand { Id = 1, Nombre = "Producto A" };

        var result = await _handler.Handle(command, default);

        Assert.IsTrue(result.Success);
        Assert.IsTrue(result.Data);
    }
}