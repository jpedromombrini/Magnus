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
        builder.HasOne(x => x.Receipt)
            .WithMany() 
            .HasForeignKey(x => x.ReceiptId)
            .OnDelete(DeleteBehavior.Restrict); 
        builder.HasMany(s => s.Installments)
            .WithOne(i => i.SaleReceipt)
            .HasForeignKey(si => si.SaleReceiptId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}