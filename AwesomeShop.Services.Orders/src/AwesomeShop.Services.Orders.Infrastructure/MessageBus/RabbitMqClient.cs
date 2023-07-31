using System.Text;
using RabbitMQ.Client;

namespace AwesomeShop.Services.Orders.Infrastructure.MessageBus;

public class RabbitMqClient : IMessageBusClient
{
    private readonly IConnection _connection;

    public RabbitMqClient( ProducerConnection producerConnection)
    {
        _connection = producerConnection.Connection;
    }
    
    
    public void Publish(object message, string routingKey, string exchange)
    {
        var channel = _connection.CreateModel();

        var payload = message.ToJsonString();
        
        var body = Encoding.UTF8.GetBytes(payload);

        channel.ExchangeDeclare(exchange, "topic", true);
            
        channel.BasicPublish(exchange, routingKey, null, body);
    }
}