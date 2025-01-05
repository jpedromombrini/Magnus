using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]

public class ClientController(
    IClientAppService clientAppService) : ControllerBase
{
    [HttpGet("getall")]
    public async Task<IEnumerable<ClientResponse>> GetAllClientsAsync(CancellationToken cancellationToken)
    {
        return await clientAppService.GetClientsAsync(cancellationToken);
    }

    [HttpGet("getbyname")]
    public async Task<IEnumerable<ClientResponse>> GetClientsByFilterAsync([FromQuery] string name,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(name))
            return [];
        return await clientAppService.GetClientsByFilterAsync(x => x.Name.ToLower().Contains(name.ToLower()),
            cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<ClientResponse> GetClientByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await clientAppService.GetClientByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddClientAsync([FromBody] CreateClientRequest request, CancellationToken cancellationToken)
    {
        await clientAppService.AddClientAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateClientAsync(Guid id, [FromBody] UpdateClientRequest request,
        CancellationToken cancellationToken)
    {
        await clientAppService.UpdateClientAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteClientAsync(Guid id, CancellationToken cancellationToken)
    {
        await clientAppService.DeleteClientAsync(id, cancellationToken);
    }
}