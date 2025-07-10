using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class AccountPayableService(
    IUnitOfWork unitOfWork,
    ISupplierService supplierService) : IAccountPayableService
{
    public async Task CreateAsync(List<AccountsPayable> accountsPayables, CancellationToken cancellationToken)
    {
        foreach (var accountsPayable in accountsPayables)
        {
            await ValidateSupplier(accountsPayable, cancellationToken);
            await ValidateExists(accountsPayable, cancellationToken);
        }
    }

    public async Task UpdateAsync(AccountsPayable accountsPayableDb, AccountsPayable accountsPayable,
        CancellationToken cancellationToken)
    {
        await ValidateSupplier(accountsPayable, cancellationToken);
        accountsPayableDb.SetDocument(accountsPayable.Document);
        accountsPayableDb.SetSupplierId(accountsPayable.SupplierId);
        accountsPayableDb.SetCreatedAt(accountsPayable.CreatedAt);
        accountsPayableDb.SetDueDate(accountsPayable.DueDate);
        accountsPayableDb.SetPaymentDate(accountsPayable.PaymentDate);
        accountsPayableDb.SetValue(accountsPayable.Value);
        accountsPayableDb.SetPaymentValue(accountsPayable.PaymentValue);
        accountsPayableDb.SetDiscount(accountsPayable.Discount);
        accountsPayableDb.SetInterest(accountsPayable.Interest);
        accountsPayableDb.SetCostCenter(accountsPayable.CostCenterId);
        accountsPayableDb.SetInstallment(accountsPayable.Installment);
        accountsPayableDb.SetInvoiceId(accountsPayable.InvoiceId);
        accountsPayableDb.SetUserPaymentId(accountsPayable.UserPaymentId);
        accountsPayableDb.SetPaymentId(accountsPayable.PaymentId);
        accountsPayableDb.SetLaboratoryId(accountsPayable.LaboratoryId);
        accountsPayableDb.SetStatus(accountsPayable.AccountPayableStatus);
    }

    private async Task ValidateSupplier(AccountsPayable accountsPayable, CancellationToken cancellationToken)
    {
        var supplier = await supplierService.GetSupplierByIdAsync(accountsPayable.SupplierId, cancellationToken);
        if (supplier == null)
            throw new EntityNotFoundException("Fornecedor não encontrado");
        accountsPayable.SetSupplierName(supplier.Name);
    }

    private async Task ValidateExists(AccountsPayable accountsPayable, CancellationToken cancellationToken)
    {
        var accountsExists =
            await unitOfWork.AccountsPayables.GetByExpressionAsync(
                x => x.SupplierId == accountsPayable.SupplierId
                     && x.Document == accountsPayable.Document
                     && x.DueDate == accountsPayable.DueDate
                , cancellationToken);
        if (accountsExists is not null)
            throw new BusinessRuleException(
                "Já existe um contas a pagar com a combinação Fornecedor, Documento e Data vendimento");
    }
}