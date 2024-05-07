using HealthChecks.API.Contracts;

namespace HealthChecks.API.Interfaces.Publishers;

public interface ICreatedClientPublisher
{
    void PublishClientCreatedEventMessage(ClientCreatedEvent clientCreatedEvent);
}
