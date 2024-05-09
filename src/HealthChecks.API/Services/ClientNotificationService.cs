using HealthChecks.API.DataTransferObjects.ClientNotification;
using HealthChecks.API.Interfaces.Mappers;
using HealthChecks.API.Interfaces.Repositories;
using HealthChecks.API.Interfaces.Services;

namespace HealthChecks.API.Services;

public sealed class ClientNotificationService(
    IClientNotificationRepository clientNotificationRepository,
    IClientNotificationMapper clientNotificationMapper) 
    : IClientNotificationService
{
    public async Task<List<ClientNotificationResponse>> GetAllByClientIdAsync(long clientId)
    {
        var clientNotificationList = await clientNotificationRepository.GetAllByClientIdAsync(clientId);

        return clientNotificationMapper.DomainListToResponseList(clientNotificationList);
    }
}
