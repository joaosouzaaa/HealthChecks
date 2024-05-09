using HealthChecks.API.Interfaces.Repositories;
using HealthChecks.API.Interfaces.Services;

namespace HealthChecks.API.Services;

public sealed class ClientNotificationService(
    IClientNotificationRepository clientNotificationRepository) 
    : IClientNotificationService
{
    public async Task GetAllByClientIdAsync(long clientId)
    {
        var clientNotificationList = await clientNotificationRepository.GetAllByClientIdAsync(clientId);

        
    }
}
