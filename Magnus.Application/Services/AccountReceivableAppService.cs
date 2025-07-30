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
    public async Task ReverseReceiptAccountPayableAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await unitOfWork.AccountsReceivables.GetByIdAsync(id, cancellationToken);
        if (account is null)
            throw new EntityNotFoundException("Contas a pagar não encontrada");
        account.ReversePayment();
        unitOfWork.AccountsReceivables.Update(account);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAccountsReceivableAsync(List<CreateAccountsReceivableRequest> request,
        CancellationToken cancellationToken)
    {
        var accounts = request.MapToEntity();
        await accountsReceivableService.CreateAsync(accounts, cancellationToken);
        foreach (var account in accounts)
            await unitOfWork.AccountsReceivables.AddAsync(account, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAccountsReceivableAsync(Guid id, UpdateAccountsReceivableRequest request,
        CancellationToken cancellationToken)
    {
        var accounts = request.MapToEntity();
        await accountsReceivableService.UpdateAsync(id, accounts, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<AccountsReceivableResponse>> GetAccountsReceivablesByFilterAsync(
        AccountsReceivableFilter filter, CancellationToken cancellationToken)
    {
        var accounts = await unitOfWork.AccountsReceivables.GetAllByExpressionAsync(x =>
            (filter.ClientId == Guid.Empty || x.ClientId == filter.ClientId) &&
            (filter.Document == 0 || x.Document == filter.Document) &&
            (filter.ReceiptId == Guid.Empty || filter.ReceiptId == x.ReceiptId) &&
            (filter.InitialDueDate == null || x.DueDate >= filter.InitialDueDate) &&
            (filter.FinalDueDate == null || x.DueDate <= filter.FinalDueDate) &&
            (filter.InitialEntryDate == null || x.CreatedAt.Date >= filter.InitialEntryDate) &&
            (filter.FinalEntryDate == null || x.CreatedAt.Date <= filter.FinalEntryDate) &&
            (filter.InitialReceiptDate == null || x.ReceiptDate >= filter.InitialReceiptDate) &&
            (filter.FinalReceiptDate == null || x.ReceiptDate <= filter.FinalReceiptDate) &&
            (filter.Status == AccountsReceivableStatus.All || filter.Status == x.Status), cancellationToken);
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

    public async Task ReceiptAccountPayableAsync(Guid id, ReceiptAccountReceivableRequest request,
        CancellationToken cancellationToken)
    {
        var account = await unitOfWork.AccountsReceivables.GetByIdAsync(id, cancellationToken);
        if (account is null)
            throw new EntityNotFoundException("Contas a receber não encontrado");
        account.SetInterest(request.Interest);
        account.SetDiscount(request.Discount);
        account.SetValue(request.Value);
        account.SetReceiptDate(request.PaymentDate);
        account.SetReceiptValue(request.PaymentValue);
        account.SetProofImage(request.ProofImage);
        account.SetStatus(AccountsReceivableStatus.Paid);
        unitOfWork.AccountsReceivables.Update(account);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}