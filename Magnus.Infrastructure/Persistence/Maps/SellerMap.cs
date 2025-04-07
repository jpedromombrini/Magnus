using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class SellerMap : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.ToTable("Seller");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .HasColumnName("Email")
            .HasColumnType("varchar(100)");  
        builder.OwnsOne(x => x.Document)
            .Property(x => x.Value)
            .HasColumnName("Document")
            .HasColumnType("varchar(14)");
        builder.OwnsOne(x => x.Phone)
            .Property(x => x.Number)
            .HasColumnName("Number")
            .IsRequired()
            .HasColumnType("varchar(15)"); 
        builder.Property(x => x.UserId)
            .IsRequired();
        builder.Property(x => x.RowVersion)
            .IsRowVersion();
    }
}