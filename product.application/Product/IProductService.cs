using product.request.Commands.v1.Product;

namespace product.application.Product;
public interface IProductService
{
    Task<entities.Product?> CreateProductAsync(CreateProductCommand product, CancellationToken cancellationToken);
}
