using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Services.Interfaces;

namespace Magnus.Application.Services;

public class AccountPayableAppService : IAccountPayableAppService
{
    public async Task AddAccountPayableAsync(CreateAccountsPayableRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAccountPayableAsync(Guid id, UpdateAccountsPayableRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AccountsPayableResponse>> GetAccountPayablesByFilterAsync(GetAccountsReceivableFilter filter, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<AccountsPayableResponse> GetAccountPayableByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAccountPayableAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}