using HealthChecks.API.DataTransferObjects.Client;
using HealthChecks.API.Entities;

namespace UnitTests.TestBuilders;

public sealed class ClientBuilder
{
    private readonly long _id = 123;
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
            Id = _id,
            Name = _name
        };

    public ClientSave SaveBuild() =>
        new(_name, 
            _description, 
            _isActive);

    public ClientResponse ResponseBuild() =>
        new(_id,
            _name,
            _description,
            _isActive);

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
