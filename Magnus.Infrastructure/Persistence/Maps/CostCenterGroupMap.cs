using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class CostCenterGroupMap : IEntityTypeConfiguration<CostCenterGroup>
{
    public void Configure(EntityTypeBuilder<CostCenterGroup> builder)
    {
        builder.ToTable("CostCenterGroup");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code)
            .IsRequired()
            .HasColumnType("varchar(2)");
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.HasMany(x => x.CostCenterSubGroups)        
            .WithOne(x => x.CostCenterGroup)
            .HasForeignKey(x => x.CostCenterGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}