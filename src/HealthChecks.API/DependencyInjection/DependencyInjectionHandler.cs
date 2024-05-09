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
        services.AddHealthChecksDependencyInjection(configuration);
        services.AddSettingsDependencyInjection();
        MappingDependencyInjection.AddMappingDependencyInjection();
        services.AddRepositoriesDependencyInjection();
        services.AddPublishersDependencyInjection();
        services.AddConsumersDependencyInjection();
        services.AddMappersDependencyInjection();
        services.AddServicesDependencyInjection();
    }
}
