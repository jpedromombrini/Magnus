using Magnus.Core.Entities;
using Magnus.Core.Repositories;
using Magnus.Infrastructure.Persistence.Contexts;

namespace Magnus.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MagnusContext _context;

    public UnitOfWork(MagnusContext context)
    {
        _context = context;
        Products = new ProductRepository(_context);
        Laboratories = new LaboratoryRepository(_context);
        Users = new UserRepository(_context);
        Clients = new ClientRepository(_context);
        Sellers = new SellerRepository(_context);
        Doctors = new DoctorRepository(_context);
        Warehouses = new WarehouseRepository(_context);
        Suppliers = new SupplierRepository(_context);
        Receipts = new ReceiptRepository(_context);
        TransferWarehouses = new TransferWarehouseRepository(_context);
        AuditProducts = new AuditProductRepository(_context);
        ProductStocks = new ProductStockRepository(_context);
    }

    public IProductRepository Products { get; }
    public ILaboratoryRepository Laboratories { get; }
    public IUserRepository Users { get; }
    public IClientRepository Clients { get; }
    public ISellerRepository Sellers { get; }
    public IDoctorRepository Doctors { get; }
    public IWarehouseRepository Warehouses { get; }
    public ISupplierRepository Suppliers { get; }
    public IReceiptRepository Receipts { get; }
    public ITransferWarehouseRepository TransferWarehouses { get; }
    public IAuditProductRepository AuditProducts { get; }
    public IProductStockRepository ProductStocks { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}