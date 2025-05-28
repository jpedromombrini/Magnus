using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;

namespace Magnus.Core.Services;

public class AccountPayableService(
    IUnitOfWork unitOfWork,
    ISupplierService supplierService) : IAccountPayableService
{
    public async Task CreateAsync(AccountsPayable accountsPayable, CancellationToken cancellationToken)
    {
        var supplier = await supplierService.GetSupplierByIdAsync(accountsPayable.SupplierId, cancellationToken);
        var accountsExists =
            await unitOfWork.AccountsPayables.GetByExpressionAsync(
                x => x.SupplierId == accountsPayable.SupplierId
                     && x.Document == accountsPayable.Document
                     && x.DueDate == accountsPayable.DueDate
                , cancellationToken);
        if (accountsExists is not null)
            throw new BusinessRuleException(
                "Já existe um contas a pagar com a combinação Fornecedor, Documento e Data vendimento");
        accountsPayable.SetSupplierName(supplier.Name);
        
        
    }
}