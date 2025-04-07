using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .HasColumnName("Email")
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.InitialDate)
            .HasColumnType("timestamp");
        builder.Property(x => x.FinalDate)
            .HasColumnType("timestamp");
        builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnType("varchar(40)");
        builder.Property(x => x.RowVersion)
            .IsRowVersion();
    }
}