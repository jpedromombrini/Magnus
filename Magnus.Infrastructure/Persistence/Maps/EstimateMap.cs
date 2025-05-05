using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class EstimateMap : IEntityTypeConfiguration<Estimate>
{
    public void Configure(EntityTypeBuilder<Estimate> builder)
    {
        builder.ToTable("Estimates");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);
        builder.Property(x => x.Observation)
            .IsRequired()
            .HasColumnType("varchar(300)")
            .HasMaxLength(500);
        builder.Property(x => x.Code)
            .IsRequired()
            .HasColumnType("integer")
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Value)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.Freight)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.FinantialDiscount)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp");
        builder.Property(x => x.ValiditAt)
            .IsRequired()
            .HasColumnType("timestamp");
        builder.HasMany(x => x.Items)
            .WithOne()
            .HasForeignKey(x => x.EstimateId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Receipts)
            .WithOne()
            .HasForeignKey(x => x.EstimateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}