namespace HealthChecks.API.Entities;

public sealed class ClientNotification
{
    public required Guid Id { get; set; }
    public required string Key { get; set; }
    public required string Message { get; set; }
    public required long ClientId { get; set; }
}
