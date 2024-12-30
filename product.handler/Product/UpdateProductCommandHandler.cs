using MediatR;
using Microsoft.Extensions.Logging;
using product.application.Product;
using product.common.Responses;
using product.request.Commands.v1.Product;

namespace product.handler.Product;
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse<bool>>
{
    private readonly ILogger<UpdateProductCommandHandler> _logger;
    private readonly IProductService _productService;

    public UpdateProductCommandHandler(ILogger<UpdateProductCommandHandler> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    public async Task<ApiResponse<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling {nameof(UpdateProductCommand)} in {nameof(UpdateProductCommandHandler)}");

        var success = await _productService.UpdateProductAsync(request, cancellationToken);

        if (!success)
        {
            _logger.LogError($"Failed to update product with Id {request.Id}");
            return ApiResponseHelper.CreateErrorResponse<bool>("Error al actualizar el producto", 400);
        }

        return ApiResponseHelper.CreateSuccessResponse(true);
    }
}