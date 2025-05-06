using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class EstimateReceiptMap : IEntityTypeConfiguration<EstimateReceipt>
{
    public void Configure(EntityTypeBuilder<EstimateReceipt> builder)
    {
        builder.ToTable("EstimateReceipt");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Receipt)
            .WithMany()
            .HasForeignKey(x => x.ReceiptId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(s => s.Installments)
            .WithOne()
            .HasForeignKey(si => si.EstimateReceiptId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}