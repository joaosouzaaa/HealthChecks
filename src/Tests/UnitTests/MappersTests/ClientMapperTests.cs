using HealthChecks.API.Entities;
using HealthChecks.API.Mappers;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;

public sealed class ClientMapperTests
{
    private readonly ClientMapper _clientMapper;

    public ClientMapperTests()
    {
        _clientMapper = new ClientMapper();
    }

    [Fact]
    public void SaveToDomain_SuccessfulScenario_ReturnsDomainObject()
    {
        // A
        var clientSave = ClientBuilder.NewObject().SaveBuild();

        // A
        var clientResult = _clientMapper.SaveToDomain(clientSave);

        // A
        Assert.Equal(clientResult.Name, clientSave.Name);
        Assert.Equal(clientResult.Description, clientSave.Description);
        Assert.Equal(clientResult.IsActive, clientSave.IsActive);
    }

    [Fact]
    public void DomainListToResponseList_SuccessfulScenario_ReturnsResponseObject()
    {
        // A
        var clientList = new List<Client>()
        {
            ClientBuilder.NewObject().DomainBuild(),
            ClientBuilder.NewObject().DomainBuild(),
            ClientBuilder.NewObject().DomainBuild(),
            ClientBuilder.NewObject().DomainBuild()
        };

        // A
        var clientResponseListResult = _clientMapper.DomainListToResponseList(clientList);

        // A
        Assert.Equal(clientResponseListResult.Count, clientList.Count);
    }
}
