using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using product.api.Configuration;
using product.common.Responses;
using product.dto.Report.v1;
using product.request.Commands.v1.Report;

namespace product.api.Controllers;

[ApiVersion(1)]
[ApiController]
[Route("touch/reports/api/v{v:apiVersion}/[controller]")]
public class ReportController : CustomController
{
    private readonly ILogger<ReportController> _logger;
    private readonly IMediator _mediator;

    public ReportController(IMediator mediator, ILogger<ReportController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [Route("generate-product-report")]
    [MapToApiVersion(1)]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<GenerateProductReportResponse>), 200)]
    public async Task<IActionResult> GenerateProductReport()
    {
        _logger.LogInformation("GenerateProductReport");
        var result = await _mediator.Send(new GenerateProductReportCommand());
        if (!result.Success)
        {
            return OkorBadRequestValidationApiResponse(result);
        }
        var fileName = $"Reporte_Productos_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
        return File(result.Data.Report, "application/pdf", fileName);
    }
}
