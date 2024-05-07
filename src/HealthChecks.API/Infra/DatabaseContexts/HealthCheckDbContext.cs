using Microsoft.EntityFrameworkCore;

namespace HealthChecks.API.Infra.DatabaseContexts;

public sealed class HealthCheckDbContext(DbContextOptions<HealthCheckDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HealthCheckDbContext).Assembly);
}
