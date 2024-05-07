using HealthChecks.API.Constants;
using HealthChecks.API.Contracts;
using HealthChecks.API.Interfaces.Publishers;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace HealthChecks.API.Infra.Publishers;

public sealed class ClientDeletedPublisher(ConnectionFactory factory) : IClientDeletedPublisher
{
    public void PublishClientDeletedEventMessage(ClientDeletedEvent clientDeletedEvent)
    {
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: QueuesConstants.ClientDeletedQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var clientDeletedEventJsonString = JsonSerializer.Serialize(clientDeletedEvent);
        var body = Encoding.UTF8.GetBytes(clientDeletedEventJsonString);

        channel.BasicPublish(exchange: "", routingKey: QueuesConstants.ClientDeletedQueue, basicProperties: null, body: body);
    }
}
