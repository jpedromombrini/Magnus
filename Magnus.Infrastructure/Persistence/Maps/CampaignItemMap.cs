using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class CampaignItemMap : IEntityTypeConfiguration<CampaignItem>
{
    public void Configure(EntityTypeBuilder<CampaignItem> builder)
    {
        builder.ToTable("CampainItem");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Campaign>()
            .WithMany(x => x.Items)
            .HasForeignKey("CampaignId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}