namespace HealthChecks.API.Contracts;

public sealed record ClientCreatedEvent(
    long ClientId,
    string Name);
