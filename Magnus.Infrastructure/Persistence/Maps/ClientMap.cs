using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class ClientMap : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Client");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(150)");
        builder.OwnsOne(x => x.Document)
            .Property(x => x.Value)
            .HasColumnName("Document")
            .IsRequired()
            .HasColumnType("varchar(14)");
        builder.OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .IsRequired(false)
            .HasColumnName("Email")
            .HasColumnType("varchar(100)");
        builder.Property(x => x.DateOfBirth)
            .HasColumnType("date");
        builder.Property(x => x.Occupation)
            .HasColumnType("varchar(50)");
        builder.OwnsOne(x => x.Address)
            .Property(x => x.City)
            .HasColumnName("City")
            .HasColumnType("varchar(50)");
        builder.OwnsOne(x => x.Address)
            .Property(x => x.State)
            .HasColumnName("State")
            .HasColumnType("varchar(2)");
        builder.OwnsOne(x => x.Address)
            .Property(x => x.Neighborhood)
            .HasColumnName("Neighborhood")
            .HasColumnType("varchar(50)");
        builder.OwnsOne(x => x.Address)
            .Property(x => x.PublicPlace)
            .HasColumnName("PublicPlace")
            .HasColumnType("varchar(100)");
        builder.OwnsOne(x => x.Address)
            .Property(x => x.Complement)
            .HasColumnName("Complement")
            .HasColumnType("varchar(50)");
        builder.OwnsOne(x => x.Address)
            .Property(x => x.Number)
            .HasColumnName("Number");
        builder.OwnsOne(x => x.Address)
            .Property(x => x.ZipCode)
            .HasColumnType("varchar(9)");
        builder.Property(x => x.RegisterNumber)
            .HasColumnType("varchar(10)");
        builder.HasMany(x => x.SocialMedias);
        builder.HasMany(x => x.Phones);
    }
}