using Microsoft.Extensions.Logging;
using Moq;
using product.application.Product;
using product.entities;
using product.handler.Product;
using product.request.Commands.v1.Product;

namespace product.test.Handlers;

[TestFixture]
public class CreateProductCommandHandlerTests
{
    private Mock<ILogger<CreateProductCommandHandler>> _loggerMock;
    private Mock<IProductService> _productServiceMock;
    private CreateProductCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<CreateProductCommandHandler>>();
        _productServiceMock = new Mock<IProductService>();
        _handler = new CreateProductCommandHandler(_loggerMock.Object, _productServiceMock.Object);
    }

    [Test]
    public async Task Handle_ReturnsSuccessResponse_WhenProductCreated()
    {
        var newProduct = new Product
        {
            Id = 1,
            Nombre = "Producto A",
            Descripcion = "Descripción A",
            Precio = 100,
            Cantidad = 10,
            Categoria = "Categoría A"
        };

        _productServiceMock.Setup(s => s.CreateProductAsync(It.IsAny<CreateProductCommand>(), default))
            .ReturnsAsync(newProduct);

        var command = new CreateProductCommand
        {
            Nombre = "Producto A",
            Descripcion = "Descripción A",
            Precio = 100,
            Cantidad = 10,
            Categoria = "Categoría A"
        };

        var result = await _handler.Handle(command, default);

        Assert.IsTrue(result.Success);
        Assert.AreEqual("Producto A", result.Data.Nombre);
    }
}