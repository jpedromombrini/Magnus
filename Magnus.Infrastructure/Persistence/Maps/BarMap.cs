using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class BarMap : IEntityTypeConfiguration<Bar>
{
    public void Configure(EntityTypeBuilder<Bar> builder)
    {
        builder.ToTable("Bar");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code)
            .IsRequired()
            .HasColumnType("varchar(14)");
    }
}
