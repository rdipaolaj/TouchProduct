using Microsoft.Extensions.DependencyInjection;
using product.internalservices.Base;
using product.internalservices.User;

namespace product.internalservices;
public static class InternalServicesConfiguration
{
    public static IServiceCollection AddInternalServicesConfiguration(this IServiceCollection services)
    {
        services.AddTransient<IBaseService, BaseService>();
        services.AddTransient<IUserService, UserService>();
        return services;
    }
}