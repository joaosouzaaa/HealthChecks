namespace HealthChecks.API.DataTransferObjects.ClientNotification;

public sealed record ClientNotificationResponse(
    Guid Id,
    string Key,
    string Message,
    long ClientId);
