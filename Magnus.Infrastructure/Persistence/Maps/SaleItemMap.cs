using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class SaleItemMap : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItem");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductName)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);
        builder.Property(x => x.TotalPrice)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.Value)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.Discount)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.Amount)
            .IsRequired();
        builder.HasOne(si => si.Sale)
            .WithMany(s => s.Items)
            .HasForeignKey(si => si.SaleId);
    }
}