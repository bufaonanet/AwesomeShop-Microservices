using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeShop.Services.Products.Application;

public static class Extensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        return services;
    }
}