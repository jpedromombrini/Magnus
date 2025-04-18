using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class InvoiceItemMap : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.ToTable("InvoiceItem");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductName)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.TotalValue)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("decimal(12,3)");
    }
}