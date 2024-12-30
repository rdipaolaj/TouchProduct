using Microsoft.Extensions.DependencyInjection;
using product.application.Email;
using product.application.Notifications;
using product.application.Notifications.Background;
using product.application.Product;

namespace product.application;
public static class ApplicationConfiguration
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
    {
        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddSingleton<INotificationService, NotificationService>();
        services.AddHostedService<NotificationBackgroundService>();

        return services;
    }
}