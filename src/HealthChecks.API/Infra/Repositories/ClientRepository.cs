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

    public async Task DeleteAsync(long id)
    {
        var client = await DbContextSet.FindAsync(id);

        DbContextSet.Remove(client!);

        await dbContext.SaveChangesAsync();
    }

    public Task<List<Client>> GetAllAsync() =>
        DbContextSet.AsNoTracking().ToListAsync();

    public void Dispose()
    {
        dbContext.Dispose();

        GC.SuppressFinalize(this);
    }
}
