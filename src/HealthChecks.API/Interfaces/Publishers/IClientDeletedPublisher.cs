using HealthChecks.API.Contracts;

namespace HealthChecks.API.Interfaces.Publishers;

public interface IClientDeletedPublisher
{
    void PublishClientDeletedEventMessage(ClientDeletedEvent clientDeletedEvent);
}
