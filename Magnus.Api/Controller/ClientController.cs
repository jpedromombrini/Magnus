using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ClientController(
    IClientAppService clientAppService,
    IConfiguration configuration) : ControllerBase
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

    [HttpGet("getbydocument")]
    public async Task<IEnumerable<ClientResponse>> GetClientsByDocumentAsync([FromQuery] string document,
        CancellationToken cancellationToken)
    {
        return await clientAppService.GetClientsByDocumentAsync(document,
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

    [HttpPost("external")]
    [AllowAnonymous]
    public async Task AddClientAnonimousAsync([FromBody] CreateClientRequest request,
        CancellationToken cancellationToken)
    {
        ValidateCsrfToken();
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

    private void ValidateCsrfToken()
    {
        var token = Request.Headers["X-CSRF-Token"].FirstOrDefault();
        var storageToken = configuration.GetSection("CsrfToken").Value;
        if (string.IsNullOrEmpty(token))
            throw new AuthenticationException("Nenhum token identificado na requisição");
        if (string.IsNullOrEmpty(storageToken))
            throw new AuthenticationException("Nenhum storage token configurado");
        if (token != storageToken)
            throw new AuthenticationException("Token enviado difere do configurado");
    }
}