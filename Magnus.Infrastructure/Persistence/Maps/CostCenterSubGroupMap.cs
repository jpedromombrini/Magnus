using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class CostCenterSubGroupMap : IEntityTypeConfiguration<CostCenterSubGroup>
{
    public void Configure(EntityTypeBuilder<CostCenterSubGroup> builder)
    {
        builder.ToTable("CostCenterSubGroup");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code)
            .IsRequired()
            .HasColumnType("varchar(5)");
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.HasMany(x =>x.CostCenters)
            .WithOne(x => x.CostCenterSubGroup)
            .HasForeignKey(x => x.CostCenterSubGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}