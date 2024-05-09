using HealthChecks.API.Interfaces.Mappers;
using HealthChecks.API.Mappers;

namespace HealthChecks.API.DependencyInjection;

internal static class MappersDependencyInjection
{
    internal static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IClientMapper, ClientMapper>();
        services.AddScoped<IClientNotificationMapper, ClientNotificationMapper>();
    }
}
