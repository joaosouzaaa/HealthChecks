using HealthChecks.API.Entities;

namespace HealthChecks.API.Interfaces.Repositories;

public interface IClientNotificationRepository
{
    Task AddAsync(ClientNotification clientNotification);
    Task<List<ClientNotification>> GetAllByClientId(long clientId);
}
