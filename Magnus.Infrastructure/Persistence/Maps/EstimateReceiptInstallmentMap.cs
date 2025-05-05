using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class EstimateReceiptInstallmentMap : IEntityTypeConfiguration<EstimateReceiptInstallment>
{
    public void Configure(EntityTypeBuilder<EstimateReceiptInstallment> builder)
    {
        builder.ToTable("EstimateReceiptInstallment");
        builder.HasKey(x => x.Id);
        builder.Property(e => e.DueDate)
            .IsRequired()
            .HasColumnType("date");
        builder.Property(e => e.PaymentDate)
            .HasColumnType("timestamp");
        builder.Property(e => e.Value)
            .IsRequired()
            .HasColumnType("numeric(10,2)");
        builder.Property(e => e.Discount)
            .IsRequired()
            .HasColumnType("numeric(10,2)");
        builder.Property(e => e.Interest)
            .IsRequired()
            .HasColumnType("numeric(10,2)");
        builder.Property(e => e.Installment)
            .IsRequired();
    }
}