using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using product.application;
using product.handler.Notification;
using product.handler.Product;
using product.infraestructure.Behaviors;
using product.internalservices;
using product.redis;
using product.requestvalidator.Product;
using product.secretsmanager;

namespace product.infraestructure.Modules;
public static class MediatorModule
{
    public static IServiceCollection AddMediatRAssemblyConfiguration(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblyContaining(typeof(CreateProductCommandHandler));
            configuration.RegisterServicesFromAssemblyContaining(typeof(UpdateProductCommandHandler));
            configuration.RegisterServicesFromAssemblyContaining(typeof(DeleteProductCommandHandler));
            configuration.RegisterServicesFromAssemblyContaining(typeof(ListProductsQueryHandler));
            configuration.RegisterServicesFromAssemblyContaining(typeof(ForceNotificationsCommandHandler));

            configuration.AddOpenBehavior(typeof(ValidatorBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(UpdateProductCommandValidator).Assembly);

        return services;
    }
    public static IServiceCollection AddCustomServicesConfiguration(this IServiceCollection services)
    {
        services.AddSecretManagerConfiguration();
        services.AddRedisServiceConfiguration();
        services.AddInternalServicesConfiguration();
        
        return services;
    }

    public static IServiceCollection AddCustomApplicationServicesConfiguration(this IServiceCollection services)
    {
        services.AddApplicationConfiguration();

        return services;
    }
}