using Microsoft.Extensions.Logging;
using Moq;
using product.application.Product;
using product.entities;
using product.handler.Product;
using product.request.Querys.v1.Product;

namespace product.test.Handlers;

[TestFixture]
public class ListProductsQueryHandlerTests
{
    private Mock<ILogger<ListProductsQueryHandler>> _loggerMock;
    private Mock<IProductService> _productServiceMock;
    private ListProductsQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<ListProductsQueryHandler>>();
        _productServiceMock = new Mock<IProductService>();
        _handler = new ListProductsQueryHandler(_loggerMock.Object, _productServiceMock.Object);
    }

    [Test]
    public async Task Handle_ReturnsProductList()
    {
        var products = new List<Product>
        {
            new Product { Id = 1, Nombre = "Producto A", Descripcion = "Descripción A", Precio = 100, Cantidad = 10, Categoria = "Categoría A" },
            new Product { Id = 2, Nombre = "Producto B", Descripcion = "Descripción B", Precio = 200, Cantidad = 20, Categoria = "Categoría B" }
        };

        _productServiceMock.Setup(s => s.GetProductsAsync(default)).ReturnsAsync(products);

        var result = await _handler.Handle(new ListProductsQuery(), default);

        Assert.IsTrue(result.Success);
        Assert.AreEqual(2, result.Data.Count());
    }
}