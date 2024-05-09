using HealthChecks.API.Contracts;

namespace HealthChecks.API.Interfaces.Publishers;

public interface IClientInactivatedPublisher
{
    void PublishClientInactivatedEventMessage(ClientInactivatedEvent clientInactivatedEvent);
}
