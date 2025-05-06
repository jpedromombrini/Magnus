using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class SaleMap : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Document)
            .IsRequired()
            .HasColumnType("integer")
            .UseIdentityColumn();
        builder.Property(x => x.ClientName)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);
        builder.Property(s => s.CreateAt)
            .IsRequired()
            .HasColumnType("timestamp");
        builder.Property(s => s.Value)
            .HasColumnType("decimal(10,2)")
            .IsRequired();
        builder.HasMany(s => s.Items)
            .WithOne(si => si.Sale)
            .HasForeignKey(si => si.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x =>x.Receipts)
            .WithOne()
            .HasForeignKey(x => x.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}