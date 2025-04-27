using Magnus.Application.Dtos.Filters;
using Magnus.Application.Dtos.Requests;
using Magnus.Application.Dtos.Responses;
using Magnus.Application.Mappers;
using Magnus.Application.Services.Interfaces;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Application.Services;

public class AccountReceivableAppService(
    IAccountsReceivableService accountsReceivableService,
    IUnitOfWork unitOfWork) : IAccountReceivableAppService
{
    public async Task AddAccountsReceivableAsync(CreateAccountsReceivableRequest request,
        CancellationToken cancellationToken)
    {
        var accounts = request.MapToEntity();
        await accountsReceivableService.CreateAsync(accounts, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAccountsReceivableAsync(Guid id, UpdateAccountsReceivableRequest request,
        CancellationToken cancellationToken)
    {
        var acoounts = request.MapToEntity();
        await accountsReceivableService.UpdateAsync(id, acoounts, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<AccountsReceivableResponse>> GetAccountsReceivablesByFilterAsync(
        GetAccountsReceivableFilter filter, CancellationToken cancellationToken)
    {
        var accounts = await unitOfWork.AccountsReceivables.GetAllByExpressionAsync(x =>
                x.DueDate >= filter.InitialDueDate &&
                x.DueDate <= filter.FinalDueDate &&
                ((filter.ClientId == null || filter.ClientId == Guid.Empty) || x.ClientId == filter.ClientId) &&
                (filter.Number == 0 || x.Document == filter.Number) &&
                (filter.Status == AccountsReceivableStatus.All || x.Status == filter.Status),
            cancellationToken);
        return accounts.MapToResponse();
    }

    public async Task<AccountsReceivableResponse> GetAccountsReceivableByIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var account = await unitOfWork.AccountsReceivables.GetByIdAsync(id, cancellationToken);
        if (account == null)
            throw new EntityNotFoundException("Nenhum contas a receber encontrado com esse id");
        return account.MapToResponse();
    }

    public async Task DeleteAccountsReceivableAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await unitOfWork.AccountsReceivables.GetByIdAsync(id, cancellationToken);
        if (account == null)
            throw new EntityNotFoundException("Nenhum contas a receber encontrado com esse id");
        unitOfWork.AccountsReceivables.Delete(account);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}