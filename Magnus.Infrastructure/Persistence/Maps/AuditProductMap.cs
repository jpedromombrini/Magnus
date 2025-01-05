using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class AuditProductMap: IEntityTypeConfiguration<AuditProduct>
{
    public void Configure(EntityTypeBuilder<AuditProduct> builder)
    {
        builder.ToTable("AuditProduct");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RecordDate)
            .IsRequired()
            .HasColumnType("timestamp");
        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("decimal(10,3)");
        builder.Property(x => x.TotalValue)
            .IsRequired()
            .HasColumnType("decimal(10,3)");
        builder.Property(x => x.Document)
            .IsRequired()
            .HasColumnType("integer");
        builder.Property(x => x.Serie)
            .IsRequired()
            .HasColumnType("integer");
        builder.Property(x => x.WarehouseId)
            .IsRequired()
            .HasColumnType("integer");
        builder.Property(x => x.Validity)
            .IsRequired()
            .HasColumnType("date");
    }
}
