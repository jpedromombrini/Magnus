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
        CostCenters = new CostCenterRepository(_context);
        CostCenterGroups = new CostCenterGroupRepository(_context);
        CostCenterSubGroups = new CostCenterSubGroupRepository(_context);
        Invoices = new InvoiceRepository(_context);
        AccountsPayables = new AccountsPayableRepository(_context);
        Payments = new PaymentRepository(_context);
        InvoicePayments = new InvoicePaymentRepository(_context);
        Estimates = new EstimateRepository(_context);
        Sales = new SaleRepository(_context);
        SaleReceipts = new SaleReceiptRepository(_context);
        AppConfigurations = new AppConfigurationRepository(_context);
        ProductPriceTables = new ProductPriceTableRepository(_context);
        Bars = new BarRepository(_context);
        AccountsReceivables = new AccountsReceivableRepository(_context);
        Freights = new FreightRepository(_context);
        StockMovements = new StockMovementRepository(_context);
        Campaigns = new CampaignRepository(_context);
        ProductGroups = new ProductGroupRepository(_context);
    }


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ExecuteInTransactionAsync(Func<Task> action, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await action();

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var result = await action();

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return result;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    #region Repositorios

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
    public ICostCenterRepository CostCenters { get; }
    public ICostCenterGroupRepository CostCenterGroups { get; }
    public ICostCenterSubGroupRepository CostCenterSubGroups { get; }
    public IInvoiceRepository Invoices { get; }
    public IAccountsPayableRepository AccountsPayables { get; }
    public IPaymentRepository Payments { get; }
    public IInvoicePaymentRepository InvoicePayments { get; }
    public IEstimateRepository Estimates { get; }
    public ISaleRepository Sales { get; }
    public ISaleReceiptRepository SaleReceipts { get; }
    public IAppConfigurationRepository AppConfigurations { get; }
    public IProductPriceTableRepository ProductPriceTables { get; set; }
    public IBarRepository Bars { get; }
    public IAccountsReceivableRepository AccountsReceivables { get; }
    public IFreightRepository Freights { get; }
    public IStockMovementRepository StockMovements { get; }
    public ICampaignRepository Campaigns { get; }
    public IProductGroupRepository ProductGroups { get; }

    #endregion
}