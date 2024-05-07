namespace HealthChecks.API.Options;

public sealed class MongoDBOptions
{
    public required string DatabaseName { get; init; }
    public required string ConnectionString { get; init; }
}
