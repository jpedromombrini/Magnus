using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class ClientPhoneMap : IEntityTypeConfiguration<ClientPhone>
{
    public void Configure(EntityTypeBuilder<ClientPhone> builder)
    {
        builder.ToTable("ClientPhone");
        builder.HasKey(p => p.Id);
        builder.OwnsOne(x => x.Phone)
            .Property(x => x.Number)
            .HasColumnName("Number")
            .IsRequired()
            .HasColumnType("varchar(15)");
        builder.Property(p => p.Description)
            .IsRequired(false)
            .HasColumnType("varchar(50)");
    }
}