using HealthChecks.API.Infra.Consumers;

namespace HealthChecks.API.DependencyInjection;

internal static class ConsumersDependencyInjection
{
    internal static void AddConsumersDependencyInjection(this IServiceCollection services)
    {
        services.AddHostedService<ClientCreatedConsumer>();
        services.AddHostedService<ClientInactivatedConsumer>();
    }
}
