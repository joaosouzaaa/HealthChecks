using HealthChecks.API.Infra.Publishers;
using HealthChecks.API.Interfaces.Publishers;

namespace HealthChecks.API.DependencyInjection;

public static class PublishersDependencyInjection
{
    public static void AddPublishersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IClientCreatedPublisher, ClientCreatedPublisher>();
        services.AddScoped<IClientDeletedPublisher, ClientDeletedPublisher>();
    }
}
