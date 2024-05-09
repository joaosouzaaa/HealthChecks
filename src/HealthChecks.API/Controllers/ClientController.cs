using HealthChecks.API.DataTransferObjects.Client;
using HealthChecks.API.Interfaces.Services;
using HealthChecks.API.Settings.NotificationSettings;
using Microsoft.AspNetCore.Mvc;

namespace HealthChecks.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ClientController(IClientService clientService) : ControllerBase
{
    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task AddAsync([FromBody] ClientSave clientSave) =>
        clientService.AddAsync(clientSave);

    [HttpPatch("inactivate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<Notification>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task InactivateAsync([FromQuery] long id) =>
        clientService.InactivateAsync(id);


    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ClientResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public Task GetAllAsync([FromQuery] bool? isActive) =>
        clientService.GetAllAsync(isActive);
}
