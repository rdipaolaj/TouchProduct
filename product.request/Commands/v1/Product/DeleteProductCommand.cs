using MediatR;
using product.common.Responses;

namespace product.request.Commands.v1.Product;
public class DeleteProductCommand : IRequest<ApiResponse<bool>>
{
    public int Id { get; set; }
}