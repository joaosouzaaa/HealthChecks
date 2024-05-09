using HealthChecks.API.DataTransferObjects.Client;

namespace HealthChecks.API.Interfaces.Services;

public interface IClientService
{
    Task AddAsync(ClientSave clientSave);
    Task InactivateAsync(long id);
    Task<List<ClientResponse>> GetAllAsync(bool? isActive);
}
