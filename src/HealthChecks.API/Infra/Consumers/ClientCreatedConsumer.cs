using HealthChecks.API.Constants;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace HealthChecks.API.Infra.Consumers;

internal sealed class ClientCreatedConsumer(ConnectionFactory factory) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: QueuesConstants.ClientCreatedQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            // consume message here

            channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        channel.BasicConsume(queue: QueuesConstants.ClientCreatedQueue, consumer: consumer);

        return Task.CompletedTask;
    }
}
