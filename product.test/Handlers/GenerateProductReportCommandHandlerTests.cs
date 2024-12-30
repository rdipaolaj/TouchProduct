using Microsoft.Extensions.Logging;
using Moq;
using product.application.Product;
using product.handler.Report;
using product.request.Commands.v1.Report;

namespace product.test.Handlers;

[TestFixture]
public class GenerateProductReportCommandHandlerTests
{
    private Mock<ILogger<GenerateProductReportCommandHandler>> _loggerMock;
    private Mock<IProductService> _productServiceMock;
    private GenerateProductReportCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<GenerateProductReportCommandHandler>>();
        _productServiceMock = new Mock<IProductService>();
        _handler = new GenerateProductReportCommandHandler(_loggerMock.Object, _productServiceMock.Object);
    }

    [Test]
    public async Task Handle_ReturnsReport_WhenReportGenerated()
    {
        var reportBytes = new byte[] { 1, 2, 3 };
        _productServiceMock.Setup(s => s.GenerateProductReportAsync(default)).ReturnsAsync(reportBytes);

        var result = await _handler.Handle(new GenerateProductReportCommand(), default);

        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Data.Report);
        Assert.AreEqual(3, result.Data.Report.Length);
    }
}