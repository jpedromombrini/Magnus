using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class SupplierMap : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Supplier");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.OwnsOne(x => x.Document)
            .Property(x => x.Value)
            .HasColumnName("Document")
            .IsRequired()
            .HasColumnType("varchar(14)");
        builder.OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .HasColumnName("Email")
            .HasColumnType("varchar(100)");     
        builder.OwnsOne(x => x.Phone)
            .Property(x => x.Number)
            .HasColumnName("PhoneNumber")
            .IsRequired()
            .HasColumnType("varchar(15)");       
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
        builder.OwnsOne(x=>x.Address)
            .Property(x =>x.PublicPlace)
            .HasColumnName("PublicPlace")
            .HasColumnType("varchar(100)");
        builder.OwnsOne(x => x.Address)
            .Property(x => x.Complement)
            .HasColumnName("Complement")
            .HasColumnType("varchar(50)");
        builder.OwnsOne(x => x.Address)
            .Property(x =>x.Number)
            .HasColumnName("AddressNumber");
        builder.OwnsOne(x => x.Address)
            .Property(x => x.ZipCode)
            .HasColumnType("varchar(9)");
    }
}