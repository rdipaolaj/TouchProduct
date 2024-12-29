using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using product.application;
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

            configuration.AddOpenBehavior(typeof(ValidatorBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);

        return services;
    }
    public static IServiceCollection AddCustomServicesConfiguration(this IServiceCollection services)
    {
        services.AddInternalServicesConfiguration();
        services.AddSecretManagerConfiguration();
        services.AddRedisServiceConfiguration();
        services.AddApplicationConfiguration();

        return services;
    }
}