using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class AccountsReceivableService(
    IClientService clientService,
    ICostCenterService costCenterService,
    ISaleReceiptService saleReceiptService,
    IUnitOfWork unitOfWork) : IAccountsReceivableService
{
    public async Task UpdateAsync(Guid id, AccountsReceivable accountsReceivable, CancellationToken cancellationToken)
    {
        var accountsExists = await unitOfWork.AccountsReceivables.GetByIdAsync(id, cancellationToken);
        if (accountsExists is null)
            throw new EntityNotFoundException("Nenhuma contas a receber encontrado com esse id");
        await Validate(accountsReceivable, cancellationToken);
        accountsExists.SetCostCenter(accountsReceivable.CostCenter);
        accountsExists.SetDueDate(accountsReceivable.DueDate);
        accountsExists.SetDocument(accountsReceivable.Document);
        accountsExists.SetValue(accountsReceivable.Value);
        accountsExists.SetDiscount(accountsReceivable.Discount);
        accountsExists.SetInstallment(accountsReceivable.Installment);
        accountsExists.SetInterest(accountsReceivable.Interest);
        accountsExists.SetSaleReceiptInstallmentId(accountsReceivable.SaleReceiptInstallmentId);
        accountsExists.SetStatus(accountsReceivable.Status);
        accountsExists.SetClientId(accountsReceivable.ClientId);
        if (!string.IsNullOrEmpty(accountsReceivable.Observation))
            accountsExists.SetObservation(accountsReceivable.Observation);
        unitOfWork.AccountsReceivables.Update(accountsExists);
    }

    public async Task<AccountsReceivable?> GetBySaleReceiptInstallmentIdAsync(Guid saleReceiptInstallmentId,
        CancellationToken cancellationToken)
    {
        return await unitOfWork.AccountsReceivables.GetByExpressionAsync(
            x => x.SaleReceiptInstallmentId == saleReceiptInstallmentId, cancellationToken);
    }

    public void RemoveRangeAsync(IEnumerable<AccountsReceivable> accountsReceivables)
    {
        unitOfWork.AccountsReceivables.DeleteAccountsReceivableRange(accountsReceivables);
    }

    public async Task CreateAsync(IEnumerable<AccountsReceivable> accountsReceivables,
        CancellationToken cancellationToken)
    {
        foreach (var accountsReceivable in accountsReceivables) await Validate(accountsReceivable, cancellationToken);
    }

    private async Task Validate(AccountsReceivable accountsReceivable, CancellationToken cancellationToken)
    {
        var client = await clientService.GetByIdAsync(accountsReceivable.ClientId, cancellationToken);
        if (client == null)
            throw new EntityNotFoundException("Nenhum cliente encontrado com esse id");
        var costCenter = await costCenterService.GetByIdAsync((Guid)accountsReceivable.CostCenterId, cancellationToken);
        if (costCenter == null)
            throw new EntityNotFoundException("Nenhum centro de custo encontrado com esse código");
        accountsReceivable.SetStatus(accountsReceivable.ReceiptDate is null
            ? AccountsReceivableStatus.Open
            : AccountsReceivableStatus.Paid);
        var accountsExists = await unitOfWork.AccountsReceivables.GetByExpressionAsync(
            x => x.ClientId == accountsReceivable.ClientId &&
                 x.Document == accountsReceivable.Document &&
                 x.DueDate == accountsReceivable.DueDate &&
                 x.Installment == accountsReceivable.Installment, cancellationToken);
        if (accountsExists is not null)
            throw new BusinessRuleException(
                "Já existe um contas a receber com a combinação cliente, documento, data vencimento e parcela");
        if (accountsReceivable.SaleReceiptInstallmentId is not null)
        {
            var installmentId = accountsReceivable.SaleReceiptInstallmentId.Value;
            var installmentExists =
                await saleReceiptService.GetSaleReceiptInstallmentByIdAsync(installmentId, cancellationToken);
            if (installmentExists is null)
                throw new BusinessRuleException("Nenhuma parcela de recebimento encontrado com esse id");
        }
    }
}