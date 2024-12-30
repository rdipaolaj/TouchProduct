using Microsoft.Extensions.Logging;
using Moq;
using product.application.Product;
using product.handler.Product;
using product.request.Commands.v1.Product;

namespace product.test.Handlers;

[TestFixture]
public class DeleteProductCommandHandlerTests
{
    private Mock<ILogger<DeleteProductCommandHandler>> _loggerMock;
    private Mock<IProductService> _productServiceMock;
    private DeleteProductCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<DeleteProductCommandHandler>>();
        _productServiceMock = new Mock<IProductService>();
        _handler = new DeleteProductCommandHandler(_loggerMock.Object, _productServiceMock.Object);
    }

    [Test]
    public async Task Handle_ReturnsSuccessResponse_WhenProductDeleted()
    {
        _productServiceMock.Setup(s => s.DeleteProductAsync(It.IsAny<int>(), default)).ReturnsAsync(true);

        var command = new DeleteProductCommand { Id = 1 };

        var result = await _handler.Handle(command, default);

        Assert.IsTrue(result.Success);
        Assert.IsTrue(result.Data);
    }
}