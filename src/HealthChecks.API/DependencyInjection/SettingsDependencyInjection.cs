using HealthChecks.API.Filters;
using HealthChecks.API.Interfaces.Settings;
using HealthChecks.API.Settings.NotificationSettings;

namespace HealthChecks.API.DependencyInjection;

internal static class SettingsDependencyInjection
{
    internal static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler, NotificationHandler>();

        services.AddScoped<NotificationFilter>();

        services.AddMvc(options => options.Filters.AddService<NotificationFilter>());
    }
}
