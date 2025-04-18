using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class EstimateItemMap : IEntityTypeConfiguration<EstimateItem>
{
    public void Configure(EntityTypeBuilder<EstimateItem> builder)
    {
        builder.ToTable("EstimateItem");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductName)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);
        builder.Property(x => x.TotalValue)
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
    }
    
}