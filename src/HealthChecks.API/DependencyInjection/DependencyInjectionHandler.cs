using HealthChecks.API.Infra.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace HealthChecks.API.DependencyInjection;

internal static class DependencyInjectionHandler
{
    internal static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingletonDependencyInjection(configuration);
        services.AddCorsDependencyInjection();

        var postgreConnectionString = configuration.GetConnectionString("PostgreConnectionString");

        services.AddDbContext<HealthCheckDbContext>(options =>
        {
            options.UseNpgsql(postgreConnectionString);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });

        services.AddOptionsDependencyInjection(configuration);
        MappingDependencyInjection.AddMappingDependencyInjection();
        services.AddRepositoriesDependencyInjection();
        services.AddPublishersDependencyInjection();
    }
}
