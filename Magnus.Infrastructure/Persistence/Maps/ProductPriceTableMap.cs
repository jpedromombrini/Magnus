using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class ProductPriceTableMap : IEntityTypeConfiguration<ProductPriceTable>
{
    public void Configure(EntityTypeBuilder<ProductPriceTable> builder)
    {
        builder.ToTable("ProductPriceTable");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.MinimalAmount)
            .IsRequired();
        builder.Property(r => r.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        builder.Property(r => r.MaximumAmount)
            .IsRequired();
    }
}