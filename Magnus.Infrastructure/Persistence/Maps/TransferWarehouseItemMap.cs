using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class TransferWarehouseItemMap : IEntityTypeConfiguration<TransferWarehouseItem>
{
    public void Configure(EntityTypeBuilder<TransferWarehouseItem> builder)
    {
        builder.ToTable("TransferWarehouseItem");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductName)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.ProductInternalCode)
            .IsRequired();
        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("decimal(10,3)");
        builder.Property(x => x.Validity)
            .IsRequired()
            .HasColumnType("date");
    }
}