using HealthChecks.API.DataTransferObjects.ClientNotification;
using HealthChecks.API.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthChecks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ClientNotificationController(IClientNotificationService clientNotificationService) : ControllerBase
{
    [HttpGet("get-all-by-client-id")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ClientNotificationResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task<List<ClientNotificationResponse>> GetAllByClientIdAsync([FromQuery] long clientId) =>
        clientNotificationService.GetAllByClientIdAsync(clientId);
}
