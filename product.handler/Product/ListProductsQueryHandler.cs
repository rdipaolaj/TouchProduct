using MediatR;
using Microsoft.Extensions.Logging;
using product.application.Product;
using product.common.Responses;
using product.dto.Product.v1;
using product.request.Querys.v1.Product;

namespace product.handler.Product;
public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, ApiResponse<IEnumerable<CreateProductResponse>>>
{
    private readonly ILogger<ListProductsQueryHandler> _logger;
    private readonly IProductService _productService;

    public ListProductsQueryHandler(ILogger<ListProductsQueryHandler> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    public async Task<ApiResponse<IEnumerable<CreateProductResponse>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling {nameof(ListProductsQuery)} in {nameof(ListProductsQueryHandler)}");

        var products = await _productService.GetProductsAsync(cancellationToken);

        var response = products.Select(p => new CreateProductResponse
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            Precio = p.Precio,
            Cantidad = p.Cantidad,
            Categoria = p.Categoria
        });

        return ApiResponseHelper.CreateSuccessResponse(response);
    }
}