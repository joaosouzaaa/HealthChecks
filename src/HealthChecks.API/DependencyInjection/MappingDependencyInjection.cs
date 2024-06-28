using HealthChecks.API.Infra.EntitiesMapping;

namespace HealthChecks.API.DependencyInjection;

internal static class MappingDependencyInjection
{
    internal static void AddMappingDependencyInjection() =>
        ClientNotificationMapping.Map();
}
