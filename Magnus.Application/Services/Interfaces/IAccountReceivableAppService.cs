using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;

namespace Magnus.Application.Services.Interfaces;

public interface IAccountReceivableAppService
{
    Task AddAccountsReceivableAsync(CreateAccountsReceivableRequest request, CancellationToken cancellationToken);
    Task UpdateAccountsReceivableAsync(Guid id, UpdateAccountsReceivableRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<AccountsReceivableResponse>> GetAccountsReceivablesByFilterAsync(GetAccountsReceivableFilter filter, CancellationToken cancellationToken);
    Task<AccountsReceivableResponse> GetAccountsReceivableByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAccountsReceivableAsync(Guid id, CancellationToken cancellationToken);
}