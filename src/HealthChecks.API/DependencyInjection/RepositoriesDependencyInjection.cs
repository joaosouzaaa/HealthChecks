using HealthChecks.API.Infra.Repositories;
using HealthChecks.API.Interfaces.Repositories;

namespace HealthChecks.API.DependencyInjection;

internal static class RepositoriesDependencyInjection
{
    internal static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IClientNotificationRepository, ClientNotificationRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
    }
}
