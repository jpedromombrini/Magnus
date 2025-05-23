using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class AccountsPayableMap: IEntityTypeConfiguration<AccountsPayable>
{
    public void Configure(EntityTypeBuilder<AccountsPayable> builder)
    {
        builder.ToTable("AccountsPayable");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Document)
            .IsRequired()
            .HasColumnType("integer");
        builder.Property(x => x.SupplierName)
            .IsRequired()
            .HasColumnType("varchar(150)");
        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp");
        builder.Property(x => x.DueDate)
            .IsRequired()
            .HasColumnType("date");
        builder.Property(x => x.PaymentDate)
            .HasColumnType("timestamp");
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
        builder.HasMany(x => x.Occurrences)
            .WithOne(x => x.AccountsPayable)
            .HasForeignKey(x => x.AccountsPayableId);
        builder.HasOne(x => x.Payment)
            .WithMany()
            .HasForeignKey(x => x.PaymentId);

    }
}
