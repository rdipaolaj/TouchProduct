using Microsoft.Extensions.DependencyInjection;
using product.application.Product;

namespace product.application;
public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
    {
        services.AddTransient<IProductService, ProductService>();

        return services;
    }
}