namespace HealthChecks.API.Entities;

public sealed class Client
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required bool IsActive { get; set; }
}
