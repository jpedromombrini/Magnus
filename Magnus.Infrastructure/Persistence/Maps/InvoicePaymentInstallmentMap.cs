using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class InvoicePaymentInstallmentMap : IEntityTypeConfiguration<InvoicePaymentInstallment>
{
    public void Configure(EntityTypeBuilder<InvoicePaymentInstallment> builder)
    {
        builder.ToTable("InvoicePaymentInstallment");
        builder.HasKey(p => p.Id);
        builder.HasOne(x => x.InvoicePayment)
            .WithMany(x => x.Installments)
            .HasForeignKey(x => x.InvoicePaymentId)
            .OnDelete(DeleteBehavior.Cascade);  
        
        builder.Property(x => x.DueDate)
            .IsRequired();  

        builder.Property(x => x.PaymentDate)
            .HasColumnType("timestamp");  

        builder.Property(x => x.Value)
            .IsRequired()
            .HasColumnType("numeric(18,2)");  

        builder.Property(x => x.Discount)
            .HasColumnType("numeric(18,2)");  

        builder.Property(x => x.Interest)
            .HasColumnType("numeric(18,2)");
        
        builder.Property(x => x.Installment)
            .IsRequired(); 
        
    }
}