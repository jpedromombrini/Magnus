using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class AccountsReceiptMap : IEntityTypeConfiguration<AccountsReceivable>
{
    public void Configure(EntityTypeBuilder<AccountsReceivable> builder)
    {
        builder.ToTable("AccountsReceivable");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp");
        builder.Property(x => x.ClientName)
            .IsRequired()
            .HasMaxLength(150);
        builder.Property(x => x.DueDate)
            .IsRequired();
        builder.Property(x => x.Value)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.PaymentValue)
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.Discount)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.Interest)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.CostCenter)
            .IsRequired()
            .HasColumnType("varchar(8)");
        builder.Property(x => x.Observation)
            .HasMaxLength(1000)
            .HasColumnType("varchar(1000)");
        
            
    }
}