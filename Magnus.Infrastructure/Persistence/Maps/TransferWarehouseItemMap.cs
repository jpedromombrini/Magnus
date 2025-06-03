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
        builder.Property(x => x.RequestedAmount)
            .IsRequired()
            .HasColumnType("integer");
        builder.Property(x => x.AutorizedAmount)
            .IsRequired()
            .HasColumnType("integer");
        builder.HasOne(x => x.TransferWarehouse)
            .WithMany()
            .HasForeignKey(x => x.TransferWarehouseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}