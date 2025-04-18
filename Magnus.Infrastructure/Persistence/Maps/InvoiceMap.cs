using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class InvoiceMap : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoice");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Number)
            .IsRequired()
            .HasColumnType("integer");
        builder.Property(x => x.Serie)
            .IsRequired()
            .HasColumnType("integer");
        builder.Property(x => x.Date)
            .IsRequired()
            .HasColumnType("timestamp");
        builder.Property(x => x.DateEntry)
            .IsRequired()
            .HasColumnType("timestamp");
        builder.Property(x => x.Key)
            .HasColumnType("varchar(44)");
        builder.Property(x => x.Deduction)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.Freight)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.Value)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.Observation)
            .HasColumnType("varchar(200)");
        builder.Property(x => x.SupplierName)
            .HasColumnType("varchar(100)");
        builder.HasMany(x => x.Items)
            .WithOne(ii => ii.Invoice)
            .HasForeignKey(ii => ii.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}