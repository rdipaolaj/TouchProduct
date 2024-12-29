using MediatR;
using product.common.Responses;
using product.dto.Product.v1;

namespace product.request.Commands.v1.Product;
public class CreateProductCommand : IRequest<ApiResponse<CreateProductResponse>>
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public string Categoria { get; set; } = string.Empty;
}
