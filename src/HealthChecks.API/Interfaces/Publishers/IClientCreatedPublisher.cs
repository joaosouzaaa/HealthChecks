using HealthChecks.API.Contracts;

namespace HealthChecks.API.Interfaces.Publishers;

public interface IClientCreatedPublisher
{
    void PublishClientCreatedEventMessage(ClientCreatedEvent clientCreatedEvent);
}
