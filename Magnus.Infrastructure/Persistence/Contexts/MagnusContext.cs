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