using MediatR;
using Microsoft.Extensions.Logging;
using product.application.Product;
using product.common.Responses;
using product.dto.Report.v1;
using product.request.Commands.v1.Report;

namespace product.handler.Report;
public class GenerateProductReportCommandHandler : IRequestHandler<GenerateProductReportCommand, ApiResponse<GenerateProductReportResponse>>
{
    private readonly ILogger<GenerateProductReportCommandHandler> _logger;
    private readonly IProductService _productService;

    public GenerateProductReportCommandHandler(ILogger<GenerateProductReportCommandHandler> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }
    public async Task<ApiResponse<GenerateProductReportResponse>> Handle(GenerateProductReportCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Handling GenerateProductReportCommand...");

            var reportBytes = await _productService.GenerateProductReportAsync(cancellationToken);

            if (reportBytes == null || !reportBytes.Any())
            {
                _logger.LogError("El reporte no se generó correctamente en GenerateProductReportCommandHandler");
                return ApiResponseHelper.CreateErrorResponse<GenerateProductReportResponse>("No se pudo generar el reporte. Verifica que existan productos para incluir en el reporte.", 400);
            }

            var response = new GenerateProductReportResponse
            {
                Report = reportBytes
            };

            return ApiResponseHelper.CreateSuccessResponse(response, "Report generated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GenerateProductReportCommandHandler");
            return ApiResponseHelper.CreateErrorResponse<GenerateProductReportResponse>("Error al generar el reporte", 500);
        }
    }
}