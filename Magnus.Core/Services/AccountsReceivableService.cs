using System.Globalization;
using Magnus.Core.Entities;
using Magnus.Core.Enumerators;
using Magnus.Core.Exceptions;
using Magnus.Core.Infrastructure;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class AccountsReceivableService(
    IClientService clientService,
    ICostCenterService costCenterService,
    ISaleReceiptService saleReceiptService,
    IUserContext userContext,
    IUnitOfWork unitOfWork) : IAccountsReceivableService
{
    public async Task UpdateAsync(Guid id, AccountsReceivable accountsReceivable, CancellationToken cancellationToken)
    {
        var accountsExists = await unitOfWork.AccountsReceivables.GetByIdAsync(id, cancellationToken);
        if (accountsExists is null)
            throw new EntityNotFoundException("Nenhuma contas a receber encontrado com esse id");
        var costCenter =
            await unitOfWork.CostCenters.GetByIdAsync((Guid)accountsExists.CostCenterId, cancellationToken);
        if (costCenter is null)
            throw new BusinessRuleException("Informe o centro de custo");
        var client = await clientService.ValidateClientAsync(accountsReceivable.ClientId, cancellationToken);
        if (client is null)
            throw new BusinessRuleException("Cliente não encontrado");
        accountsReceivable.SetClient(client);
        await SetOcurrence(accountsExists, accountsReceivable, cancellationToken);

        accountsExists.SetCostCenter(costCenter);
        accountsExists.SetDueDate(accountsReceivable.DueDate);
        accountsExists.SetDocument(accountsReceivable.Document);
        accountsExists.SetValue(accountsReceivable.Value);
        accountsExists.SetDiscount(accountsReceivable.Discount);
        accountsExists.SetInstallment(accountsReceivable.Installment);
        accountsExists.SetInterest(accountsReceivable.Interest);
        accountsExists.SetSaleReceiptInstallmentId(accountsReceivable.SaleReceiptInstallmentId);
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

    public async Task CreateAsync(IEnumerable<AccountsReceivable> accountsReceivables, Client client,
        CancellationToken cancellationToken)
    {
        foreach (var accountsReceivable in accountsReceivables)
        {
            await Validate(accountsReceivable, client, cancellationToken);
            await unitOfWork.AccountsReceivables.AddAsync(accountsReceivable, cancellationToken);
        }
    }

    private async Task Validate(AccountsReceivable accountsReceivable, Client client,
        CancellationToken cancellationToken)
    {
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

    private async Task SetOcurrence(AccountsReceivable before, AccountsReceivable after,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(userContext.GetUserId(), cancellationToken);
        if (user is null)
            throw new EntityNotFoundException("Usuário não encontrado");

        if (after.Client.Id != before.Client.Id)
        {
            var fact = $"Alteração: Cliente | Antigo: {before.Client.Name} | Novo: {after.Client.Name}";
            var occurrence = new AccountsReceivableOccurence(before.Id, user.Id, fact);
            occurrence.SetAccountsReceivable(before);
            occurrence.SetUser(user);
            before.AddOcurrence(occurrence);
        }

        if (after.Value != before.Value)
        {
            var fact =
                $"Alteração: Valor | Antigo: {before.Value.ToString("C", new CultureInfo("pt-BR"))} | Novo: {after.Value.ToString("C", new CultureInfo("pt-BR"))}";
            var occurrence = new AccountsReceivableOccurence(before.Id, user.Id, fact);
            occurrence.SetAccountsReceivable(before);
            occurrence.SetUser(user);
            before.AddOcurrence(occurrence);
        }

        if (after.Interest != before.Interest)
        {
            var fact =
                $"Alteração: Juros | Antigo: {before.Interest.ToString("C", new CultureInfo("pt-BR"))} | Novo: {after.Interest.ToString("C", new CultureInfo("pt-BR"))}";
            var occurrence = new AccountsReceivableOccurence(before.Id, user.Id, fact);
            occurrence.SetAccountsReceivable(before);
            occurrence.SetUser(user);
            before.AddOcurrence(occurrence);
        }

        if (after.Discount != before.Discount)
        {
            var fact =
                $"Alteração: Desconto | Antigo: {before.Discount.ToString("C", new CultureInfo("pt-BR"))} | Novo: {after.Discount.ToString("C", new CultureInfo("pt-BR"))}";
            var occurrence = new AccountsReceivableOccurence(before.Id, user.Id, fact);
            occurrence.SetAccountsReceivable(before);
            occurrence.SetUser(user);
            before.AddOcurrence(occurrence);
        }

        if (after.DueDate != before.DueDate)
        {
            var fact =
                $"Alteração: Vencimento | Antigo: {before.DueDate.ToString("dd/MM/yyyy")} | Novo: {after.DueDate.ToString("dd/MM/yyyy")}";
            var occurrence = new AccountsReceivableOccurence(before.Id, user.Id, fact);
            occurrence.SetAccountsReceivable(before);
            occurrence.SetUser(user);
            before.AddOcurrence(occurrence);
        }

        if (after.Installment != before.Installment)
        {
            var fact =
                $"Alteração: Parcela | Antigo: {before.Installment} | Novo: {after.Installment}";
            var occurrence = new AccountsReceivableOccurence(before.Id, user.Id, fact);
            occurrence.SetAccountsReceivable(before);
            occurrence.SetUser(user);
            before.AddOcurrence(occurrence);
        }
    }
}