using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class PriceRuleMap : IEntityTypeConfiguration<PriceRule>
{
    public void Configure(EntityTypeBuilder<PriceRule> builder)
    {
        builder.ToTable("PriceRule");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.From)
            .IsRequired()
            .HasColumnType("integer");
        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(10,2)");
            
    }
}
