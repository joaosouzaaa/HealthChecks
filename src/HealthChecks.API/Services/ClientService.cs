using FluentValidation;
using HealthChecks.API.Contracts;
using HealthChecks.API.DataTransferObjects.Client;
using HealthChecks.API.Entities;
using HealthChecks.API.Interfaces.Mappers;
using HealthChecks.API.Interfaces.Publishers;
using HealthChecks.API.Interfaces.Repositories;
using HealthChecks.API.Interfaces.Services;
using HealthChecks.API.Interfaces.Settings;

namespace HealthChecks.API.Services;

public sealed class ClientService(
    IClientRepository clientRepository,
    IClientMapper clientMapper,
    IValidator<Client> validator,
    IClientCreatedPublisher clientCreatedPublisher,
    IClientDeletedPublisher clientDeletedPublisher,
    INotificationHandler notificationHandler)
    : IClientService
{
    public async Task AddAsync(ClientSave clientSave)
    {
        var client = clientMapper.SaveToDomain(clientSave);

        if (!await ValidateAsync(client))
        {
            return;
        }

        await clientRepository.AddAsync(client);

        var clientCreatedEvent = new ClientCreatedEvent(client.Id, client.Name);
        clientCreatedPublisher.PublishClientCreatedEventMessage(clientCreatedEvent);
    }

    public async Task DeleteAsync(long id)
    {
        if(!await clientRepository.ExistsAsync(id))
        {
            notificationHandler.AddNotification("Not found", "Client was not found.");

            return;
        }

        await clientRepository.DeleteAsync(id);

        var clientDeletedEvent = new ClientDeletedEvent(id);
        clientDeletedPublisher.PublishClientDeletedEventMessage(clientDeletedEvent);
    }

    public async Task<List<ClientResponse>> GetAllAsync()
    {
        var clientList = await clientRepository.GetAllAsync();

        return clientMapper.DomainListToResponseList(clientList);
    }

    private async Task<bool> ValidateAsync(Client client)
    {
        var validationResult = await validator.ValidateAsync(client);

        if (validationResult.IsValid)
        {
            return true;
        }

        foreach(var error in  validationResult.Errors)
        {
            notificationHandler.AddNotification(error.PropertyName, error.ErrorMessage);
        }

        return false;
    }
}
