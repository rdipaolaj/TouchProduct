using MediatR;
using Microsoft.Extensions.Logging;
using product.application.Product;
using product.common.Responses;
using product.request.Commands.v1.Product;

namespace product.handler.Product;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse<bool>>
{
    private readonly ILogger<DeleteProductCommandHandler> _logger;
    private readonly IProductService _productService;

    public DeleteProductCommandHandler(ILogger<DeleteProductCommandHandler> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    public async Task<ApiResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling {nameof(DeleteProductCommand)} in {nameof(DeleteProductCommandHandler)}");

        var success = await _productService.DeleteProductAsync(request.Id, cancellationToken);

        if (!success)
        {
            _logger.LogError($"Failed to delete product with Id {request.Id}");
            return ApiResponseHelper.CreateErrorResponse<bool>("Error al eliminar el producto", 400);
        }

        return ApiResponseHelper.CreateSuccessResponse(true);
    }
}