using PdfSharp.Drawing;
using PdfSharp.Pdf;
using product.data.Repository.Interfaces;
using product.request.Commands.v1.Product;

namespace product.application.Product;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<entities.Product?> CreateProductAsync(CreateProductCommand product, CancellationToken cancellationToken)
    {
        var newProduct = new entities.Product
        {
            Nombre = product.Nombre,
            Descripcion = product.Descripcion,
            Precio = product.Precio,
            Cantidad = product.Cantidad,
            Categoria = product.Categoria
        };

        return await _productRepository.CreateProductAsync(newProduct, cancellationToken);
    }
    public async Task<bool> UpdateProductAsync(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductByIdAsync(command.Id, cancellationToken);
        if (product == null) return false;

        product.Nombre = command.Nombre;
        product.Descripcion = command.Descripcion;
        product.Precio = command.Precio;
        product.Cantidad = command.Cantidad;
        product.Categoria = command.Categoria;

        await _productRepository.UpdateProductAsync(product, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteProductAsync(int id, CancellationToken cancellationToken)
    {
        return await _productRepository.DeleteProductAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<entities.Product>> GetProductsAsync(CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductsAsync(cancellationToken);
    }

    public async Task<IEnumerable<entities.Product>> GetProductsForReportAsync(CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductsForReportAsync(cancellationToken);
    }

    public async Task<byte[]> GenerateProductReportAsync(CancellationToken cancellationToken)
    {
        var products = await GetProductsForReportAsync(cancellationToken);

        if (!products.Any())
        {
            return Array.Empty<byte>();
        }

        return await Task.Run(() =>
        {
            using var document = new PdfDocument();
            GenerateReportPages(document, products);

            using var memoryStream = new MemoryStream();
            document.Save(memoryStream);
            return memoryStream.ToArray();
        });
    }

    private void GenerateReportPages(PdfDocument document, IEnumerable<entities.Product> products)
    {
        var page = document.AddPage();
        var graphics = XGraphics.FromPdfPage(page);

        double margin = 20;
        double yPoint = margin + 40;

        // Fuentes
        var titleFont = new XFont("Arial", 18);
        var headerFont = new XFont("Arial", 12);
        var contentFont = new XFont("Arial", 10);

        // Título
        graphics.DrawString("Reporte de Productos", titleFont, XBrushes.Black,
            new XRect(0, margin, page.Width, page.Height), XStringFormats.TopCenter);

        // Tabla
        yPoint += 30;
        DrawTableHeader(graphics, headerFont, yPoint);
        yPoint += 25;

        foreach (var product in products)
        {
            if (yPoint > page.Height - 50)
            {
                page = document.AddPage();
                graphics = XGraphics.FromPdfPage(page);
                yPoint = margin + 40;
                DrawTableHeader(graphics, headerFont, yPoint);
                yPoint += 25;
            }

            DrawProductRow(graphics, product, contentFont, yPoint, page.Width);
            yPoint += 25;
        }
    }

    private void DrawTableHeader(XGraphics graphics, XFont headerFont, double yPoint)
    {
        double[] columnWidths = { 80, 150, 70, 60, 100, 120 };
        string[] headers = { "Nombre", "Descripción", "Precio", "Cantidad", "Categoría", "Fecha" };

        double xPoint = 20;
        for (int i = 0; i < headers.Length; i++)
        {
            graphics.DrawRectangle(XPens.Black, xPoint, yPoint, columnWidths[i], 20);
            graphics.DrawString(headers[i], headerFont, XBrushes.Black,
                new XRect(xPoint, yPoint, columnWidths[i], 20), XStringFormats.Center);
            xPoint += columnWidths[i];
        }
    }

    private void DrawProductRow(XGraphics graphics, entities.Product product, XFont contentFont, double yPoint, double pageWidth)
    {
        double[] columnWidths = { 80, 150, 70, 60, 100, 120 };
        string[] values = {
            product.Nombre,
            product.Descripcion,
            $"{product.Precio:C}",
            product.Cantidad.ToString(),
            product.Categoria,
            product.CreateProduct.ToString(),
        };

        double xPoint = 20;
        for (int i = 0; i < values.Length; i++)
        {
            graphics.DrawRectangle(XPens.Black, xPoint, yPoint, columnWidths[i], 20);
            graphics.DrawString(TrimTextToFit(values[i], contentFont, columnWidths[i] - 6, graphics),
                contentFont, XBrushes.Black,
                new XRect(xPoint + 2, yPoint + 2, columnWidths[i], 20), XStringFormats.TopLeft);
            xPoint += columnWidths[i];
        }
    }

    private string TrimTextToFit(string text, XFont font, double maxWidth, XGraphics graphics)
    {
        while (graphics.MeasureString(text, font).Width > maxWidth && text.Length > 0)
        {
            text = text[..^1];
        }

        return text.Length > 0 ? text : "...";
    }
}
