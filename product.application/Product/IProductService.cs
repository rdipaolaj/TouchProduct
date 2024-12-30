using product.request.Commands.v1.Product;

namespace product.application.Product;
public interface IProductService
{
    Task<entities.Product?> CreateProductAsync(CreateProductCommand product, CancellationToken cancellationToken);
    Task<bool> UpdateProductAsync(UpdateProductCommand command, CancellationToken cancellationToken);
    Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<entities.Product>> GetProductsAsync(CancellationToken cancellationToken);
    Task<byte[]> GenerateProductReportAsync(CancellationToken cancellationToken);
}
