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
    public async Task<bool> UpdateProductAsync(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(command.Id, cancellationToken);
        if (product == null) return false;

        product.Nombre = command.Nombre;
        product.Descripcion = command.Descripcion;
        product.Precio = command.Precio;
        product.Cantidad = command.Cantidad;
        product.Categoria = command.Categoria;

        await _productRepository.UpdateProductAsync(product, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken)
    {
        return await _productRepository.DeleteProductAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<entities.Product>> GetProductsAsync(CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductsAsync(cancellationToken);
    }
}
