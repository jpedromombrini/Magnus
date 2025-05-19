using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class InvoicePaymentMap : IEntityTypeConfiguration<InvoicePayment>
{
    public void Configure(EntityTypeBuilder<InvoicePayment> builder)
    {
        builder.ToTable("InvoicePayment");
        builder.HasKey(p => p.Id);
        builder.HasMany(x => x.Installments)
            .WithOne(x => x.InvoicePayment)
            .HasForeignKey(x => x.InvoicePaymentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}