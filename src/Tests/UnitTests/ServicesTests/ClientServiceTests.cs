using FluentValidation;
using FluentValidation.Results;
using HealthChecks.API.Contracts;
using HealthChecks.API.DataTransferObjects.Client;
using HealthChecks.API.Entities;
using HealthChecks.API.Interfaces.Mappers;
using HealthChecks.API.Interfaces.Publishers;
using HealthChecks.API.Interfaces.Repositories;
using HealthChecks.API.Interfaces.Settings;
using HealthChecks.API.Services;
using Moq;
using UnitTests.TestBuilders;

namespace UnitTests.ServicesTests;

public sealed class ClientServiceTests
{
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly Mock<IClientMapper> _clientMapperMock;
    private readonly Mock<IValidator<Client>> _validatorMock;
    private readonly Mock<IClientCreatedPublisher> _clientCreatedPublisherMock;
    private readonly Mock<IClientDeletedPublisher> _clientDeletedPublisherMock;
    private readonly Mock<INotificationHandler> _notificationHandlerMock;
    private readonly ClientService _clientService;

    public ClientServiceTests()
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        _clientMapperMock = new Mock<IClientMapper>();
        _validatorMock = new Mock<IValidator<Client>>();
        _clientCreatedPublisherMock = new Mock<IClientCreatedPublisher>();
        _clientDeletedPublisherMock = new Mock<IClientDeletedPublisher>();
        _notificationHandlerMock = new Mock<INotificationHandler>();
        _clientService = new ClientService(_clientRepositoryMock.Object, _clientMapperMock.Object, _validatorMock.Object,
            _clientCreatedPublisherMock.Object, _clientDeletedPublisherMock.Object, _notificationHandlerMock.Object);
    }

    [Fact]
    public async Task AddAsync_SuccessfulScenario_ReturnsSuccessfulTask()
    {
        // A
        var clientSave = ClientBuilder.NewObject().SaveBuild();

        var client = ClientBuilder.NewObject().DomainBuild();
        _clientMapperMock.Setup(c => c.SaveToDomain(It.IsAny<ClientSave>()))
            .Returns(client);

        var validationResult = new ValidationResult();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        _clientRepositoryMock.Setup(c => c.AddAsync(It.IsAny<Client>()));
        _clientCreatedPublisherMock.Setup(c => c.PublishClientCreatedEventMessage(It.IsAny<ClientCreatedEvent>()));

        // A
        await _clientService.AddAsync(clientSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _clientRepositoryMock.Verify(c => c.AddAsync(It.IsAny<Client>()), Times.Once());
        _clientCreatedPublisherMock.Verify(c => c.PublishClientCreatedEventMessage(It.IsAny<ClientCreatedEvent>()), Times.Once());
    }

    [Fact]
    public async Task AddAsync_EntityInvalid_ReturnsUnsuccessfulTask()
    {
        // A
        var clientSave = ClientBuilder.NewObject().SaveBuild();

        var client = ClientBuilder.NewObject().DomainBuild();
        _clientMapperMock.Setup(c => c.SaveToDomain(It.IsAny<ClientSave>()))
            .Returns(client);

        var validationFailureList = new List<ValidationFailure>()
        {
            new("teste", "tes")
        };
        var validationResult = new ValidationResult()
        {
            Errors = validationFailureList
        };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<Client>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(validationResult);

        // A
        await _clientService.AddAsync(clientSave);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(validationResult.Errors.Count));
        _clientRepositoryMock.Verify(c => c.AddAsync(It.IsAny<Client>()), Times.Never());
        _clientCreatedPublisherMock.Verify(c => c.PublishClientCreatedEventMessage(It.IsAny<ClientCreatedEvent>()), Times.Never());
    }

    [Fact]
    public async Task DeleteAsync_SuccessfulScenario_ReturnsSuccessfulTask()
    {
        // A
        var id = 123;

        _clientRepositoryMock.Setup(c => c.ExistsAsync(It.IsAny<long>()))
            .ReturnsAsync(true);

        _clientRepositoryMock.Setup(c => c.DeleteAsync(It.IsAny<long>()));
        _clientDeletedPublisherMock.Setup(c => c.PublishClientDeletedEventMessage(It.IsAny<ClientDeletedEvent>()));

        // A
        await _clientService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        _clientRepositoryMock.Verify(c => c.DeleteAsync(It.IsAny<long>()), Times.Once());
        _clientDeletedPublisherMock.Verify(c => c.PublishClientDeletedEventMessage(It.IsAny<ClientDeletedEvent>()), Times.Once());
    }

    [Fact]
    public async Task DeleteAsync_EntityDoesNotExist_ReturnsUnsuccessfulTask()
    {
        // A
        var id = 123;

        _clientRepositoryMock.Setup(c => c.ExistsAsync(It.IsAny<long>()))
            .ReturnsAsync(false);

        // A
        await _clientService.DeleteAsync(id);

        // A
        _notificationHandlerMock.Verify(n => n.AddNotification(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        _clientRepositoryMock.Verify(c => c.DeleteAsync(It.IsAny<long>()), Times.Never());
        _clientDeletedPublisherMock.Verify(c => c.PublishClientDeletedEventMessage(It.IsAny<ClientDeletedEvent>()), Times.Never());
    }

    [Fact]
    public async Task GetAllAsync_SuccessfulScenario_ReturnsResponseList()
    {
        // A
        var clientList = new List<Client>()
        {
            ClientBuilder.NewObject().DomainBuild(),
            ClientBuilder.NewObject().DomainBuild(),
            ClientBuilder.NewObject().DomainBuild()
        };
        _clientRepositoryMock.Setup(c => c.GetAllAsync())
            .ReturnsAsync(clientList);

        var clientResponseList = new List<ClientResponse>()
        {
            ClientBuilder.NewObject().ResponseBuild()
        };
        _clientMapperMock.Setup(c => c.DomainListToResponseList(It.IsAny<List<Client>>()))
            .Returns(clientResponseList);

        // A
        var clientResponseListResult = await _clientService.GetAllAsync();

        // A
        Assert.Equal(clientResponseListResult.Count, clientResponseList.Count);
    }
}
