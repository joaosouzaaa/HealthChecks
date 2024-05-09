using HealthChecks.API.DataTransferObjects.ClientNotification;
using HealthChecks.API.Entities;

namespace UnitTests.TestBuilders;

public sealed class ClientNotificationBuilder
{
    private readonly long _clientId = 123;
    private readonly Guid _id = Guid.NewGuid();
    private readonly string _key = "test";
    private readonly string _message = "random";

    public static ClientNotificationBuilder NewObject() =>
        new();

    public ClientNotification DomainBuild() =>
        new()
        {
            ClientId = _clientId,
            Id = _id,
            Key = _key,
            Message = _message
        };

    public ClientNotificationResponse ResponseBuild() =>
        new(_id,
            _key,
            _message,
            _clientId);
}
