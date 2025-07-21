using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class AppConfigurationMap : IEntityTypeConfiguration<AppConfiguration>
{
    public void Configure(EntityTypeBuilder<AppConfiguration> builder)
    {
        builder.ToTable("AppConfiguration");
        builder.HasKey(x => x.Id);
        builder.HasOne(ar => ar.CostCenterSale)
            .WithMany()
            .HasForeignKey(ar => ar.CostCenterSaleId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}