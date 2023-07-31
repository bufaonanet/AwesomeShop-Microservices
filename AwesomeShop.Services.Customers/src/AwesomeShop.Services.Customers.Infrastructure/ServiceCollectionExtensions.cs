using AwesomeShop.Services.Customers.Core.Entities;
using AwesomeShop.Services.Customers.Core.Repositories;
using AwesomeShop.Services.Customers.Infrastructure.MessageBus;
using AwesomeShop.Services.Customers.Infrastructure.Persistence;
using AwesomeShop.Services.Customers.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using RabbitMQ.Client;

namespace AwesomeShop.Services.Customers.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        services.AddSingleton<MongoDbOptions>(sp =>
        {
            var configuration = sp.GetService<IConfiguration>();
            var options = new MongoDbOptions();

            // options.Database = configuration.GetValue<string>("");
            // options.ConnectionString = configuration.GetValue<string>("");

            configuration!.GetSection("Mongo").Bind(options);

            return options;
        });

        services.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetService<MongoDbOptions>();

            return new MongoClient(options!.ConnectionString);
        });

        services.AddTransient<IMongoDatabase>(sp =>
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            var options = sp.GetService<MongoDbOptions>();
            var mongoClient = sp.GetService<IMongoClient>();

            return mongoClient.GetDatabase(options.Database);
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddMongoRepository<Customer>("customers");
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }

    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        var connectionFactory = new ConnectionFactory
        {
            // HostName = "amqp://guest:guest@rabbitmq:5672",
            HostName = "localhost",
            Port = 5672
        };

        var connection = connectionFactory.CreateConnection("customers-service-producer");

        services.AddSingleton(new ProducerConnection(connection));
        services.AddSingleton<IMessageBusClient, RabbitMqClient>();
        services.AddTransient<IEventProcessor, EventProcessor>();

        return services;
    }

    private static IServiceCollection AddMongoRepository<T>(
        this IServiceCollection services, string collection) where T : IEntityBase
    {
        services.AddScoped<IMongoRepository<T>>(f =>
        {
            var mongoDatabase = f.GetRequiredService<IMongoDatabase>();

            return new MongoRepository<T>(mongoDatabase, collection);
        });

        return services;
    }
}