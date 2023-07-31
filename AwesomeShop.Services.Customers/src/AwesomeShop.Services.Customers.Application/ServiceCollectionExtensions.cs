using System.Reflection;
using AwesomeShop.Services.Customers.Application.Subscribers;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomeShop.Services.Customers.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        return services;
    }
    
    public static IServiceCollection AddSubscribers(this IServiceCollection services) {
        services.AddHostedService<CustomerCreatedSubscriber>();     

        return services;
    }
}