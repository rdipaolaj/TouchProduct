using product.entities;

namespace product.data.Repository.Interfaces;
public interface IProductRepository
{
    Task<IEnumerable<Product?>?> GetProductsAsync(CancellationToken cancellationToken);
    Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
    Task<Product?> CreateProductAsync(Product product, CancellationToken cancellationToken);
    Task<Product?> UpdateProductAsync(Product product, CancellationToken cancellationToken);
    Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken);
}
