using HealthChecks.API.DataTransferObjects.Client;
using HealthChecks.API.Entities;

namespace HealthChecks.API.Interfaces.Mappers;

public interface IClientMapper
{
    Client SaveToDomain(ClientSave clientSave);
    List<ClientResponse> DomainListToResponseList(List<Client> clientList);
}
