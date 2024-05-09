using HealthChecks.API.Entities;
using HealthChecks.API.Mappers;
using UnitTests.TestBuilders;

namespace UnitTests.MappersTests;

public sealed class ClientNotificationMapperTests
{
    private readonly ClientNotificationMapper _clientNotificationMapper;

    public ClientNotificationMapperTests()
    {
        _clientNotificationMapper = new ClientNotificationMapper();
    }

    [Fact]
    public void DomainListToResponseList_SuccessfulScenario_ReturnsResponseList()
    {
        // A
        var clientNotificationList = new List<ClientNotification>()
        {
            ClientNotificationBuilder.NewObject().DomainBuild(),
            ClientNotificationBuilder.NewObject().DomainBuild(),
            ClientNotificationBuilder.NewObject().DomainBuild()
        };

        // A
        var clientNotificationResponseListResult = _clientNotificationMapper.DomainListToResponseList(clientNotificationList);

        // A
        Assert.Equal(clientNotificationResponseListResult.Count, clientNotificationList.Count);
    }
}
