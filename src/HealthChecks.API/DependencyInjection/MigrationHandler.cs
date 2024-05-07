using HealthChecks.API.Infra.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace HealthChecks.API.DependencyInjection;

internal static class MigrationHandler
{
    internal static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var healthCheckDbContext = scope.ServiceProvider.GetRequiredService<HealthCheckDbContext>();

        try
        {
            healthCheckDbContext.Database.Migrate();
        }
        catch
        {
            throw;
        }
    }
}
