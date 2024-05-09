namespace HealthChecks.API.DataTransferObjects.Client;

public sealed record ClientSave(
    string Name,
    string Description,
    bool IsActive);
