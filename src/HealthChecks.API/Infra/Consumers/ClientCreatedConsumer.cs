﻿using HealthChecks.API.Constants;
using HealthChecks.API.Contracts;
using HealthChecks.API.Entities;
using HealthChecks.API.Interfaces.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace HealthChecks.API.Infra.Consumers;

internal sealed class ClientCreatedConsumer(
    ConnectionFactory factory,
    IServiceScopeFactory serviceScopeFactory)
    : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: QueuesConstants.ClientCreatedQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, eventArgs) =>
        {
            await AddClientNotificationAsync(eventArgs);

            channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        channel.BasicConsume(queue: QueuesConstants.ClientCreatedQueue, consumer: consumer);

        return Task.CompletedTask;
    }

    private async Task AddClientNotificationAsync(BasicDeliverEventArgs eventArgs)
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var clientCreatedEvent = JsonSerializer.Deserialize<ClientCreatedEvent>(message)!;

        using var scope = serviceScopeFactory.CreateScope();
        var clientNotificationRepository = scope.ServiceProvider.GetRequiredService<IClientNotificationRepository>();

        await clientNotificationRepository.AddAsync(
            new ClientNotification()
            {
                ClientId = clientCreatedEvent.ClientId,
                Id = Guid.NewGuid(),
                Key = "Client Created",
                Message = $"The client {clientCreatedEvent.Name} was created."
            });
    }
}
