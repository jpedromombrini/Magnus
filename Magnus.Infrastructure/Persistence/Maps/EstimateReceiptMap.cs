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
        builder.HasMany(s => s.Installments);
    }
}