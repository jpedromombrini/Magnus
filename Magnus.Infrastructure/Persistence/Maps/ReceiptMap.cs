using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class ReceiptMap : IEntityTypeConfiguration<Receipt>
{
    public void Configure(EntityTypeBuilder<Receipt> builder)
    {
        builder.ToTable("Receipt");
        builder.HasKey(x => x.Id);      
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.RowVersion)
            .IsRowVersion();
    }
}