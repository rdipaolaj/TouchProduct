using Microsoft.Extensions.DependencyInjection;
using product.secretsmanager.Service;

namespace product.secretsmanager;
public static class SecretManagerConfiguration
{
    public static IServiceCollection AddSecretManagerConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<ISecretManagerService, SecretManagerService>();

        return services;
    }
}