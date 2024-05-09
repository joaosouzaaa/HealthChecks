namespace HealthChecks.API.DataTransferObjects.Client;

public sealed record ClientResponse(
    long Id,
    string Name,
    string Description,
    bool IsActive);
