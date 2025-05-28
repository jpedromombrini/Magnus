using Magnus.Core.Entities;

namespace Magnus.Core.Services.Interfaces;

public interface ISupplierService
{
    Task AddSupplierAsync(Supplier supplier, CancellationToken cancellationToken);
    Task UpdateSupplierAsync(Guid id, Supplier supplier, CancellationToken cancellationToken);
    Task <Supplier> GetSupplierByIdAsync(Guid id, CancellationToken cancellationToken);
}