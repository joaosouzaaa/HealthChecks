using HealthChecks.API.Constants;
using HealthChecks.API.Options;
using RabbitMQ.Client;

namespace HealthChecks.API.DependencyInjection;

internal static class SingletonDependencyInjection
{
    internal static void AddSingletonDependencyInjection(this IServiceCollection services, IConfiguration configuration) =>
        services.AddSingleton(c =>
            new ConnectionFactory()
            {
                Uri = new Uri(configuration.GetSection(OptionsConstants.RabbitMQSection).Get<RabbitMQOptions>()!.Uri)
            });
}
