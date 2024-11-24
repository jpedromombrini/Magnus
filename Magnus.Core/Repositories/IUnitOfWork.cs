using Magnus.Core.Entities;

namespace Magnus.Core.Repositories;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    ILaboratoryRepository Laboratories { get; }
    IUserRepository Users { get; }
    IClientRepository Clients { get; }
    ISellerRepository Sellers { get; }
    IDoctorRepository Doctors { get; }
    IWarehouseRepository Warehouses { get; }
    ISupplierRepository Suppliers { get; }
    IReceiptRepository Receipts { get; }
    ITransferWarehouseRepository TransferWarehouses { get; }
    IAuditProductRepository AuditProducts { get; }
    IProductStockRepository ProductStocks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}