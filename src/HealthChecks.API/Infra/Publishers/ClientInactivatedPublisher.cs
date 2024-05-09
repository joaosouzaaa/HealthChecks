using HealthChecks.API.Constants;
using HealthChecks.API.Contracts;
using HealthChecks.API.Interfaces.Publishers;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace HealthChecks.API.Infra.Publishers;

public sealed class ClientInactivatedPublisher(ConnectionFactory factory) : IClientInactivatedPublisher
{
    public void PublishClientInactivatedEventMessage(ClientInactivatedEvent clientInactivatedEvent)
    {
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        var clientDeletedEventJsonString = JsonSerializer.Serialize(clientInactivatedEvent);
        var body = Encoding.UTF8.GetBytes(clientDeletedEventJsonString);

        channel.BasicPublish(exchange: "", routingKey: QueuesConstants.ClientInactivatedQueue, basicProperties: null, body: body);
    }
}
