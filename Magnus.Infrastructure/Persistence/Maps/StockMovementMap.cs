using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class StockMovementMap : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.ToTable("StockMovements");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatAt)
            .IsRequired()
            .HasColumnType("timestamp");
        builder.Property(x => x.Observation)
            .IsRequired(false)
            .HasColumnType("varchar(500)");
        builder.Property(x => x.WarehouseName)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.UserId)
            .IsRequired();
        builder.Property(x => x.ProductId)
            .IsRequired();
    }
}