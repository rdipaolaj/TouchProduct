using Microsoft.Extensions.DependencyInjection;
using product.internalservices;
using product.redis;
using product.secretsmanager;

namespace product.infraestructure.Modules;
public static class MediatorModule
{
    public static IServiceCollection AddMediatRAssemblyConfiguration(this IServiceCollection services)
    {
        //services.AddMediatR(configuration =>
        //{

        //    configuration.AddOpenBehavior(typeof(ValidatorBehavior<,>));
        //});

        //services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);

        return services;
    }
    public static IServiceCollection AddCustomServicesConfiguration(this IServiceCollection services)
    {
        services.AddInternalServicesConfiguration();
        services.AddSecretManagerConfiguration();
        services.AddRedisServiceConfiguration();

        return services;
    }
}