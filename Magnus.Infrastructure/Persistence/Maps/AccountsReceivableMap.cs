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
        builder.Property(x => x.ReceiptDate)
            .IsRequired(false)
            .HasColumnType("timestamp");
        builder.Property(x => x.DueDate)
            .IsRequired();
        builder.Property(x => x.Value)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.ReceiptValue)
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.Discount)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.Interest)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
        builder.Property(x => x.Observation)
            .HasMaxLength(1000)
            .HasColumnType("varchar(1000)");
        builder.HasOne(ar => ar.Client)
            .WithMany()
            .HasForeignKey(ar => ar.ClientId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(ar => ar.Receipt)
            .WithMany()
            .HasForeignKey(ar => ar.ReceiptId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(ar => ar.CostCenter)
            .WithMany()
            .HasForeignKey(ar => ar.CostCenterId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}