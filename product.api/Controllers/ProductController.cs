using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using product.api.Configuration;
using product.common.Responses;
using product.dto.Product.v1;
using product.request.Commands.v1.Product;
using product.request.Querys.v1.Product;

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
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<CreateProductResponse>), 200)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
    {
        _logger.LogInformation("CreateProduct");
        var result = await _mediator.Send(request);
        return OkorBadRequestValidationApiResponse(result);
    }

    [HttpPut]
    [Route("update-product")]
    [MapToApiVersion(1)]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
    public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand request)
    {
        _logger.LogInformation("UpdateProduct");
        var result = await _mediator.Send(request);
        return OkorBadRequestValidationApiResponse(result);
    }

    [HttpDelete]
    [MapToApiVersion(1)]
    [Authorize]
    [Route("delete-product/{id}")]
    [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        _logger.LogInformation("DeleteProduct");
        var result = await _mediator.Send(new DeleteProductCommand { Id = id });
        return OkorBadRequestValidationApiResponse(result);
    }

    [HttpGet]
    [MapToApiVersion(1)]
    [Authorize]
    [Route("list-products")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CreateProductResponse>>), 200)]
    public async Task<IActionResult> ListProducts()
    {
        _logger.LogInformation("ListProducts");
        var result = await _mediator.Send(new ListProductsQuery());
        return OkorBadRequestValidationApiResponse(result);
    }
}
