using HealthChecks.API.DataTransferObjects.ClientNotification;
using HealthChecks.API.Entities;
using HealthChecks.API.Interfaces.Mappers;

namespace HealthChecks.API.Mappers;

public sealed class ClientNotificationMapper : IClientNotificationMapper
{
    public List<ClientNotificationResponse> DomainListToResponseList(List<ClientNotification> clientNotificationList) =>
        clientNotificationList.Select(DomainToResponse).ToList();

    private ClientNotificationResponse DomainToResponse(ClientNotification clientNotification) =>
        new(clientNotification.Id,
            clientNotification.Key,
            clientNotification.Message,
            clientNotification.ClientId);
}
