using FluentValidation;
using HealthChecks.API.Entities;
using HealthChecks.API.Interfaces.Publishers;
using HealthChecks.API.Interfaces.Repositories;
using HealthChecks.API.Interfaces.Services;
using HealthChecks.API.Interfaces.Settings;

namespace HealthChecks.API.Services;

public sealed class ClientService(
    IClientRepository clientRepository,
    IValidator<Client> validator,
    IClientCreatedPublisher clientCreatedPublisher,
    IClientDeletedPublisher clientDeletedPublisher,
    INotificationHandler notificationHandler)
    : IClientService
{
}
