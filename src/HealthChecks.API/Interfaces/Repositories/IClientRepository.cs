using HealthChecks.API.Entities;

namespace HealthChecks.API.Interfaces.Repositories;

public interface IClientRepository
{
    Task AddAsync(Client client);
    Task<bool> ExistsAsync(long id);
    Task DeleteAsync(long id);
    Task<List<Client>> GetAllAsync();
}
