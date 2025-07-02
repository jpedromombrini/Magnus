using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnus.Api.Controller;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AccountsPayableController(IAccountPayableAppService accountPayableAppService) : ControllerBase
{
    [HttpGet("getbyfilter")]
    public async Task<IEnumerable<AccountsPayableResponse>> GetAccountsPayablesByFilterAsync(
        [FromQuery] GetAccountsPayableFilter filter,
        CancellationToken cancellationToken)
    {
        return await accountPayableAppService.GetAccountPayablesByFilterAsync(filter, cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<AccountsPayableResponse> GetAccountsPayableByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        return await accountPayableAppService.GetAccountPayableByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddAccountsPayableAsync([FromBody] CreateAccountsPayableRequest request,
        CancellationToken cancellationToken)
    {
        await accountPayableAppService.AddAccountPayableAsync(request, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateAccountsPayableAsync(Guid id, [FromBody] UpdateAccountsPayableRequest request,
        CancellationToken cancellationToken)
    {
        await accountPayableAppService.UpdateAccountPayableAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteAccountsPayableAsync(Guid id, CancellationToken cancellationToken)
    {
        await accountPayableAppService.DeleteAccountPayableAsync(id, cancellationToken);
    }
}