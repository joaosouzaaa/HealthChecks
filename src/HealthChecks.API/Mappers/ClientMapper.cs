using HealthChecks.API.DataTransferObjects.Client;
using HealthChecks.API.Entities;
using HealthChecks.API.Interfaces.Mappers;

namespace HealthChecks.API.Mappers;

public sealed class ClientMapper : IClientMapper
{
    public Client SaveToDomain(ClientSave clientSave) =>
        new()
        {
            Name = clientSave.Name,
            Description = clientSave.Description,
            IsActive = clientSave.IsActive
        };

    public List<ClientResponse> DomainListToResponseList(List<Client> clientList) =>
        clientList.Select(DomainToResponse).ToList();

    private ClientResponse DomainToResponse(Client client) =>
        new(client.Id,
            client.Name,
            client.Description,
            client.IsActive);
}
