using HealthChecks.API.Settings.NotificationSettings;

namespace HealthChecks.API.Interfaces.Settings;

public interface INotificationHandler
{
    List<Notification> GetNotifications();
    bool HasNotifications();
    void AddNotification(string key, string message);
}
