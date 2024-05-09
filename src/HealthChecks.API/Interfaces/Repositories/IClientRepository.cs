using HealthChecks.API.Entities;

namespace HealthChecks.API.Interfaces.Repositories;

public interface IClientRepository
{
    Task AddAsync(Client client);
    Task<bool> ExistsAsync(long id);
    Task InactivateAsync(long id);
    Task<List<Client>> GetAllAsync(bool? isActive);
}
