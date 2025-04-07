using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class LaboratoryMap : IEntityTypeConfiguration<Laboratory>
{
    public void Configure(EntityTypeBuilder<Laboratory> builder)
    {
        builder.ToTable("Laboratory");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code)
            .IsRequired()
            .HasColumnType("integer")
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.RowVersion)
            .IsRowVersion();
    }
}
