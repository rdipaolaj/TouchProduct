using product.data.Repository.Interfaces;
using product.request.Commands.v1.Product;

namespace product.application.Product;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<entities.Product?> CreateProductAsync(CreateProductCommand product, CancellationToken cancellationToken)
    {
        var newProduct = new entities.Product
        {
            Nombre = product.Nombre,
            Descripcion = product.Descripcion,
            Precio = product.Precio,
            Cantidad = product.Cantidad,
            Categoria = product.Categoria
        };

        return await _productRepository.CreateProductAsync(newProduct, cancellationToken);
    }
}
