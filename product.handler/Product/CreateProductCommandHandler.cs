using MediatR;
using Microsoft.Extensions.Logging;
using product.application.Product;
using product.common.Responses;
using product.dto.Product.v1;
using product.request.Commands.v1.Product;

namespace product.handler.Product;
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<CreateProductResponse>>
{
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IProductService _productService;

    public CreateProductCommandHandler(
        ILogger<CreateProductCommandHandler> logger,
        IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    public async Task<ApiResponse<CreateProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling {nameof(CreateProductCommand)} in {nameof(CreateProductCommandHandler)}");

        var newProduct = await _productService.CreateProductAsync(request, cancellationToken);

        if (newProduct == null)
        {
            _logger.LogError($"Error creating product in {nameof(CreateProductCommandHandler)}");
            return ApiResponseHelper.CreateErrorResponse<CreateProductResponse>("Error al crear el producto", 400);
        }

        var response = new CreateProductResponse
        {
            Id = newProduct.Id,
            Nombre = newProduct.Nombre,
            Descripcion = newProduct.Descripcion,
            Precio = newProduct.Precio,
            Cantidad = newProduct.Cantidad,
            Categoria = newProduct.Categoria
        };

        return ApiResponseHelper.CreateSuccessResponse(response);
    }
}
