using System.Text;
using AwesomeShop.Services.Customers.Infrastructure.MessageBus;
using AwesomeShop.Services.Customers.Infrastructure.MessageBus.IntegrationEvents;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AwesomeShop.Services.Customers.Application.Subscribers;

public class CustomerCreatedSubscriber : BackgroundService
{
    private readonly IConnection _connection;

    private readonly IModel _channel;

    //private const string QueueName = "customers-service/customers.CustomerCreatedIntegration";
    private const string QueueName = "customer-service/customer-created";
    private const string Exchange = "customer-service";
    private const string RoutingKey = "customer-created";

    public CustomerCreatedSubscriber(ProducerConnection producerConnection)
    {
        _connection = producerConnection.Connection;

        _channel = _connection.CreateModel();
        _channel.QueueDeclare(QueueName, true, false, false, null);
        _channel.QueueBind(QueueName, Exchange, RoutingKey);
    }


    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (sender, eventArgs) =>
        {
            var contentArray = eventArgs.Body.ToArray();
            var contentString = Encoding.UTF8.GetString(contentArray);
            var message = JsonConvert.DeserializeObject<CustomerCreatedIntegration>(contentString);

            Console.WriteLine($"Message CustomerCreatedIntegration received with Id {message.Id}");

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(QueueName, false, consumer);

        return Task.CompletedTask;
    }
}