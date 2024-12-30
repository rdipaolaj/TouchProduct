using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using product.data.Repository.Interfaces;
using product.entities;

namespace product.data.Repository;
public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Product?> CreateProductAsync(Product product, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Creating product {product.Nombre} in {nameof(CreateProductAsync)}");
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync();
            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(CreateProductAsync)}: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Deleting product with id {id} in {nameof(DeleteProductAsync)}");
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                _logger.LogWarning($"Product with id {id} not found in {nameof(DeleteProductAsync)}");
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(DeleteProductAsync)}: {ex.Message}");
            return false;
        }
    }

    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Getting product with id {id} in {nameof(GetProductByIdAsync)}");
            return await _context.Products.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(GetProductByIdAsync)}: {ex.Message}");
            return null;
        }
    }

    public async Task<IEnumerable<Product?>?> GetProductsAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Getting all products in {nameof(GetProductsAsync)}");
            return await _context.Products.ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(GetProductsAsync)}: {ex.Message}");
            return null;
        }
    }

    public async Task<Product?> UpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Updating product with id {product.Id} in {nameof(UpdateProductAsync)}");
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
            {
                _logger.LogWarning($"Product with id {product.Id} not found in {nameof(UpdateProductAsync)}");
                return null;
            }

            existingProduct.Nombre = product.Nombre;
            existingProduct.Descripcion = product.Descripcion;
            existingProduct.Precio = product.Precio;
            existingProduct.Categoria = product.Categoria;

            await _context.SaveChangesAsync();
            return existingProduct;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(UpdateProductAsync)}: {ex.Message}");
            return null;
        }
    }

    public async Task<IEnumerable<Product>> GetLowStockProductsAsync(int threshold, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Getting products with inventory below {threshold} in {nameof(GetLowStockProductsAsync)}");
            return await _context.Products
                                 .Where(p => p.Cantidad < threshold)
                                 .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(GetLowStockProductsAsync)}: {ex.Message}");
            return Enumerable.Empty<Product>();
        }
    }

    public async Task<IEnumerable<Product>> GetProductsForReportAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation($"Fetching products for report in {nameof(GetProductsForReportAsync)}");
            return await _context.Products
                                 .OrderBy(p => p.Categoria)
                                 .ThenBy(p => p.Nombre)
                                 .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in {nameof(GetProductsForReportAsync)}: {ex.Message}");
            return Enumerable.Empty<Product>();
        }
    }
}
