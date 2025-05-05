using Magnus.Core.Entities;
using Magnus.Core.Exceptions;
using Magnus.Core.Repositories;
using Magnus.Core.Services.Interfaces;
using Magnus.Core.ValueObjects;

namespace Magnus.Core.Services;

public class SupplierService(IUnitOfWork unitOfWork) : ISupplierService
{
    public async Task AddSupplierAsync(Supplier supplier, CancellationToken cancellationToken)
    {
        var supplierDb =
            await unitOfWork.Suppliers.GetByExpressionAsync(x => x.Document.Value == supplier.Document.Value,
                cancellationToken);
        if (supplierDb is not null)
            throw new ApplicationException("Já existe um fornecedor com esse documento");
        supplierDb =
            await unitOfWork.Suppliers.GetByExpressionAsync(x => supplier.Email != null && x.Email.Address.ToLower() == supplier.Email.Address.ToLower(), cancellationToken);
        if (supplierDb is not null)
            throw new ApplicationException("Já existe um fornecedor com esse email");
        supplierDb = await unitOfWork.Suppliers.GetByExpressionAsync(
            x => x.Name.ToLower() == supplier.Name.ToLower(), cancellationToken);
        if (supplierDb is not null)
            throw new ApplicationException("Já existe um fornecedor com esse nome");
        await unitOfWork.Suppliers.AddAsync(supplier, cancellationToken);
    }

    public async Task UpdateSupplierAsync(Guid id, Supplier supplier, CancellationToken cancellationToken)
    {
        var supplierDb = await unitOfWork.Suppliers.GetByIdAsync(id, cancellationToken);
        if (supplierDb is null)
            throw new EntityNotFoundException(id);
        supplierDb.SetName(supplier.Name);
        if(supplier.Phone != null)
            supplierDb.SetPhone(supplier.Phone);
        if (supplier.Address != null)
            supplierDb.SetAddress(supplier.Address);
        supplierDb.SetDocument(supplier.Document);
        if(supplier.Email != null)
            supplierDb.SetEmail(supplier.Email);
        unitOfWork.Suppliers.Update(supplierDb);
    }
}