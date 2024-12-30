using MediatR;
using product.common.Responses;
using product.dto.Product.v1;

namespace product.request.Querys.v1.Product;
public class ListProductsQuery : IRequest<ApiResponse<IEnumerable<CreateProductResponse>>>
{
}