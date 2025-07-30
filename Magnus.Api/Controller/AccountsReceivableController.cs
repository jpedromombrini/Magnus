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
public class AccountsReceivableController(IAccountReceivableAppService accountReceivableAppService) : ControllerBase
{
    [HttpGet("getbyfilter")]
    public async Task<IEnumerable<AccountsReceivableResponse>> GetAccountsReceivablesByFilterAsync(
        [FromQuery] AccountsReceivableFilter filter,
        CancellationToken cancellationToken)
    {
        return await accountReceivableAppService.GetAccountsReceivablesByFilterAsync(filter, cancellationToken);
    }

    [HttpGet("{id:guid}")]
    public async Task<AccountsReceivableResponse> GetAccountsReceivableByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        return await accountReceivableAppService.GetAccountsReceivableByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task AddAccountsReceivableAsync([FromBody] List<CreateAccountsReceivableRequest> request,
        CancellationToken cancellationToken)
    {
        await accountReceivableAppService.AddAccountsReceivableAsync(request, cancellationToken);
    }

    [HttpPost("{id:guid}/receipt")]
    public async Task ReceiptAccountsAsync(Guid id, [FromBody] ReceiptAccountReceivableRequest request,
        CancellationToken cancellationToken)
    {
        await accountReceivableAppService.ReceiptAccountPayableAsync(id, request, cancellationToken);
    }

    [HttpPost("{id:guid}/reversereceipt")]
    public async Task AddAccountsPayableAsync(Guid id, CancellationToken cancellationToken)
    {
        await accountReceivableAppService.ReverseReceiptAccountPayableAsync(id, cancellationToken);
    }

    [HttpPut("{id:guid}")]
    public async Task UpdateAccountsReceivableAsync(Guid id, [FromBody] UpdateAccountsReceivableRequest request,
        CancellationToken cancellationToken)
    {
        await accountReceivableAppService.UpdateAccountsReceivableAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id:guid}")]
    public async Task DeleteAccountsReceivableAsync(Guid id, CancellationToken cancellationToken)
    {
        await accountReceivableAppService.DeleteAccountsReceivableAsync(id, cancellationToken);
    }
}