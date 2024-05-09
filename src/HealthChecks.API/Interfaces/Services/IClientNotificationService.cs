using HealthChecks.API.DataTransferObjects.ClientNotification;

namespace HealthChecks.API.Interfaces.Services;

public interface IClientNotificationService
{
    Task<List<ClientNotificationResponse>> GetAllByClientIdAsync(long clientId);
}
