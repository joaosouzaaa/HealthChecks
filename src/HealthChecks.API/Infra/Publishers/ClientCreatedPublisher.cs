using HealthChecks.API.Constants;
using HealthChecks.API.Contracts;
using HealthChecks.API.Interfaces.Publishers;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace HealthChecks.API.Infra.Publishers;

public sealed class ClientCreatedPublisher(ConnectionFactory factory) : IClientCreatedPublisher
{
    public void PublishClientCreatedEventMessage(ClientCreatedEvent clientCreatedEvent)
    {
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: QueuesConstants.ClientCreatedQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var clientCreatedEventJsonString = JsonSerializer.Serialize(clientCreatedEvent);
        var body = Encoding.UTF8.GetBytes(clientCreatedEventJsonString);

        channel.BasicPublish(exchange: "", routingKey: QueuesConstants.ClientCreatedQueue, basicProperties: null, body: body);
    }
}
