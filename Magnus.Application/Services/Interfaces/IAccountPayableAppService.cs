using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;

namespace Magnus.Application.Services.Interfaces;

public interface IAccountPayableAppService
{
    Task AddAccountPayableAsync(CreateAccountsPayableRequest request, CancellationToken cancellationToken);
    Task UpdateAccountPayableAsync(Guid id, UpdateAccountsPayableRequest request, CancellationToken cancellationToken);

    Task<IEnumerable<AccountsPayableResponse>> GetAccountPayablesByFilterAsync(GetAccountsReceivableFilter filter,
        CancellationToken cancellationToken);

    Task<AccountsPayableResponse> GetAccountPayableByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAccountPayableAsync(Guid id, CancellationToken cancellationToken);
}