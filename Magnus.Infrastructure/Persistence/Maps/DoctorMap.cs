using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class DoctorMap : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("Doctor");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code)
            .IsRequired()
            .HasColumnType("integer")
            .UseIdentityColumn();
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.Crm)
            .HasColumnType("varchar(20)");
        builder.Property(x => x.RowVersion)
            .IsRowVersion();
    }
}