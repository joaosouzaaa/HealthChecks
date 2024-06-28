using HealthChecks.API.DataTransferObjects.ClientNotification;
using HealthChecks.API.Entities;
using HealthChecks.API.Interfaces.Mappers;
using HealthChecks.API.Interfaces.Repositories;
using HealthChecks.API.Services;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.ServicesTests;

public sealed class ClientNotificationServiceTests
{
    private readonly Mock<IClientNotificationRepository> _clientNotificationRepositoryMock;
    private readonly Mock<IClientNotificationMapper> _clientNotificationMapperMock;
    private readonly ClientNotificationService _clientNotificationService;

    public ClientNotificationServiceTests()
    {
        _clientNotificationRepositoryMock = new Mock<IClientNotificationRepository>();
        _clientNotificationMapperMock = new Mock<IClientNotificationMapper>();
        _clientNotificationService = new ClientNotificationService(_clientNotificationRepositoryMock.Object, 
            _clientNotificationMapperMock.Object);
    }

    [Fact]
    public async Task GetAllByClientIdAsync_SuccessfulScenario_ReturnsResponseList()
    {
        // A
        var clientId = 123;

        var clientNotificationList = new List<ClientNotification>()
        {
            ClientNotificationBuilder.NewObject().DomainBuild(),
            ClientNotificationBuilder.NewObject().DomainBuild(),
            ClientNotificationBuilder.NewObject().DomainBuild(),
            ClientNotificationBuilder.NewObject().DomainBuild()
        };
        _clientNotificationRepositoryMock.Setup(c => c.GetAllByClientIdAsync(It.IsAny<long>()))
            .ReturnsAsync(clientNotificationList);

        var clientNotificationResponseList = new List<ClientNotificationResponse>()
        {
            ClientNotificationBuilder.NewObject().ResponseBuild(),
            ClientNotificationBuilder.NewObject().ResponseBuild()
        };
        _clientNotificationMapperMock.Setup(c => c.DomainListToResponseList(It.IsAny<List<ClientNotification>>()))
            .Returns(clientNotificationResponseList);

        // A
        var clientNotificationResponseListResult = await _clientNotificationService.GetAllByClientIdAsync(clientId);

        // A
        Assert.Equal(clientNotificationResponseListResult.Count, clientNotificationResponseList.Count);
    }
}
