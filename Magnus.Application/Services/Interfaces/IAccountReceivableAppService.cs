using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;

namespace Magnus.Application.Services.Interfaces;

public interface IAccountReceivableAppService
{
    Task ReceiptAccountReceivableAsync(Guid id, ReceiptAccountReceivableRequest request,
        CancellationToken cancellationToken);

    Task ReverseReceiptAccountReceivableAsync(Guid id, CancellationToken cancellationToken);

    Task AddAccountsReceivableAsync(List<CreateAccountsReceivableRequest> request, CancellationToken cancellationToken);

    Task UpdateAccountsReceivableAsync(Guid id, UpdateAccountsReceivableRequest request,
        CancellationToken cancellationToken);

    Task<IEnumerable<AccountsReceivableResponse>> GetAccountsReceivablesByFilterAsync(AccountsReceivableFilter filter,
        CancellationToken cancellationToken);

    Task<AccountsReceivableResponse> GetAccountsReceivableByIdAsync(Guid id, CancellationToken cancellationToken);
    Task RenegociateAsync(Guid id, AccountsReceivableRenegociateRequest request, CancellationToken cancellationToken);
    Task DeleteAccountsReceivableAsync(Guid id, CancellationToken cancellationToken);
}