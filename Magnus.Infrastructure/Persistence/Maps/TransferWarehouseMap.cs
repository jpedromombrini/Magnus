using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class TransferWarehouseMap : IEntityTypeConfiguration<TransferWarehouse>
{
    public void Configure(EntityTypeBuilder<TransferWarehouse> builder)
    {
        builder.ToTable("TransferWarehouse");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp");
        builder.Property(x => x.UserId)
            .IsRequired();
        builder.Property(x => x.UserName)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.WarehouseDestinyId)
            .IsRequired();
        builder.Property(x => x.WarehouseDestinyName)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.WarehouseOriginId)
            .IsRequired();
        builder.Property(x => x.WarehouseOriginName)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.HasMany(tw => tw.Items)
            .WithOne() 
            .HasForeignKey(twi => twi.TransferWarehouseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}