using HealthChecks.API.DataTransferObjects.ClientNotification;
using HealthChecks.API.Entities;

namespace HealthChecks.API.Interfaces.Mappers;

public interface IClientNotificationMapper
{
    List<ClientNotificationResponse> DomainListToResponseList(List<ClientNotification> clientNotificationList);
}
