using MediatR;
using product.common.Responses;
using product.dto.Report.v1;

namespace product.request.Commands.v1.Report;
public class GenerateProductReportCommand : IRequest<ApiResponse<GenerateProductReportResponse>>
{
}