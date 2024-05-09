using HealthChecks.API.Entities;
using HealthChecks.API.Infra.DatabaseContexts;
using HealthChecks.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HealthChecks.API.Infra.Repositories;

public sealed class ClientRepository(HealthCheckDbContext dbContext) : IClientRepository, IDisposable
{
    private DbSet<Client> DbContextSet => dbContext.Set<Client>();

    public Task AddAsync(Client client)
    {
        DbContextSet.Add(client);

        return dbContext.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(long id) =>
        DbContextSet.AnyAsync(c => c.Id == id);

    public Task InactivateAsync(long id) =>
        DbContextSet.Where(c => c.Id == id)
                    .ExecuteUpdateAsync(c => c.SetProperty(c => c.IsActive, false));

    public Task<List<Client>> GetAllAsync(bool? isActive) =>
        DbContextSet.AsNoTracking()
                    .Where(c => isActive == null || c.IsActive == isActive)
                    .ToListAsync();

    public void Dispose()
    {
        dbContext.Dispose();

        GC.SuppressFinalize(this);
    }
}
