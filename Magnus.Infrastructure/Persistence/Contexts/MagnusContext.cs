using System.Reflection;
using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Magnus.Infrastructure.Persistence.Contexts;
public class MagnusContext(DbContextOptions<MagnusContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Laboratory> Laboratories { get; set; }
    public DbSet<Bar> Bars { get; set; }
    public DbSet<PriceRule> PriceRules { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientPhone> ClientPhones { get; set; }
    public DbSet<ClientSocialMedia> ClientSocialMedias { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<TransferWarehouse> TransferWarehouses { get; set; }
    public DbSet<TransferWarehouseItem> TransferWarehouseItems { get; set; }
    public DbSet<AuditProduct> AuditProducts { get; set; }
    public DbSet<ProductStock> ProductStocks { get; set; }
    public DbSet<CostCenter> CostCenters { get; set; }
    public DbSet<CostCenterSubGroup> CostCenterSubGroups { get; set; }
    public DbSet<CostCenterGroup> CostCenterGroups { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceItem> InvoiceItems { get; set; }
    public DbSet<AccountsPayable> AccountsPayables { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<InvoicePayment> InvoicePayments { get; set; }
    public DbSet<InvoicePaymentInstallment> InvoicePaymentInstallments { get; set; }
    public DbSet<Estimate> Estimates { get; set; }
    public DbSet<EstimateItem> EstimateItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}