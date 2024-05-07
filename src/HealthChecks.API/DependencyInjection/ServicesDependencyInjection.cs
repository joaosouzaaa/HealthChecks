using HealthChecks.API.Interfaces.Services;
using HealthChecks.API.Services;

namespace HealthChecks.API.DependencyInjection;

internal static class ServicesDependencyInjection
{
    internal static void AddServicesDependencyInjection(this IServiceCollection services) 
    {
        services.AddScoped<IClientService, ClientService>();
    }
}
