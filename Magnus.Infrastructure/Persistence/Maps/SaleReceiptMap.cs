using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class SaleReceiptMap : IEntityTypeConfiguration<SaleReceipt>
{
    public void Configure(EntityTypeBuilder<SaleReceipt> builder)
    {
        builder.ToTable("SaleReceipt");
        builder.HasKey(x => x.Id);
        builder.HasMany(s => s.Installments)
            .WithOne(si => si.SaleReceipt)
            .HasForeignKey(si => si.SaleReceiptId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}