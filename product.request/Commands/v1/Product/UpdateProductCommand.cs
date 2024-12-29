namespace product.request.Commands.v1.Product;
public class UpdateProductCommand
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }
    public string Categoria { get; set; } = string.Empty;
}
