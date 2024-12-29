using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using product.api.Configuration;
using product.common.Responses;
using product.dto.Product.v1;
using product.request.Commands.v1.Product;

namespace product.api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("touch/product/api/v{v:apiVersion}/[controller]")]
public class ProductController : CustomController
{
    private readonly ILogger<ProductController> _logger;
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator, ILogger<ProductController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost]
    [MapToApiVersion(1)]
    [Route("create-product")]
    [ProducesResponseType(typeof(ApiResponse<CreateProductResponse>), 400)]
    [ProducesResponseType(typeof(ApiResponse<CreateProductResponse>), 200)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
    {
        _logger.LogInformation("CreateProduct");
        var result = await _mediator.Send(request);
        return OkorBadRequestValidationApiResponse(result);
    }

}
