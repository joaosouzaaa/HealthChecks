using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace HealthChecks.API.DependencyInjection;

internal static class HealthChecksDependencyInjection
{
    internal static void AddHealthChecksDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddRabbitMQ(configuration["RabbitMQCredentials:Uri"]!, sslOption: null)
            .AddMongoDb(configuration["MongoDB:ConnectionString"]!)
            .AddNpgSql(configuration.GetConnectionString("PostgreConnectionString")!);
    }

    internal static void UseHealthChecksDependencyInjection(this WebApplication app)
    {
        app.MapHealthChecks("/health", new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    }
}
