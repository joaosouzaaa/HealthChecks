using HealthChecks.API.Entities;

namespace UnitTests.TestBuilders;

public sealed class ClientBuilder
{
    private readonly bool _isActive = true;
    private string _description = "teste with more";
    private string _name = "name";

    public static ClientBuilder NewObject() =>
        new();

    public Client DomainBuild() =>
        new()
        {
            IsActive = _isActive,
            Description = _description,
            Id = 123,
            Name = _name
        };

    public ClientBuilder WithDescription(string description)
    {
        _description = description;

        return this;
    }

    public ClientBuilder WithName(string name)
    {
        _name = name;

        return this;
    }
}
