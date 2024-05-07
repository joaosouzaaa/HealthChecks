using HealthChecks.API.Constants;
using HealthChecks.API.Options;

namespace HealthChecks.API.DependencyInjection;

internal static class OptionsDependencyInjection
{
    internal static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDBOptions>(configuration.GetSection(OptionsConstants.MongoDBSection));
        services.Configure<RabbitMQOptions>(configuration.GetSection(OptionsConstants.RabbitMQSection));
    }
}
