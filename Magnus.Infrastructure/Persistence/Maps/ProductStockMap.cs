using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class ProductStockMap : IEntityTypeConfiguration<ProductStock>
{
    public void Configure(EntityTypeBuilder<ProductStock> builder)
    {
        builder.ToTable("ProductStock");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ValidityDate)
            .IsRequired()
            .HasColumnType("date");
        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("decimal(10,3)");
        builder.Property(x => x.WarehouseId)
            .IsRequired();
        builder.Property(x => x.WarehouseName)
            .IsRequired()
            .HasColumnType("varchar(100)");
    }
}