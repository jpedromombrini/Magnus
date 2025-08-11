using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class CampainMap : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        builder.ToTable("Campain");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasColumnType("varchar(500)");
        builder.Property(x => x.InitialDate)
            .IsRequired()
            .HasColumnType("date");
        builder.Property(x => x.FinalDate)
            .IsRequired()
            .HasColumnType("date");
        builder.Property(x => x.Active)
            .IsRequired();
    }
}