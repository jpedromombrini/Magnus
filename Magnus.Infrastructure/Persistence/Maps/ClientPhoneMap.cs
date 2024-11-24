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
            .HasColumnType("varchar(14)");
        builder.Property(p => p.Description)
            .IsRequired()
            .HasColumnType("varchar(50)");
    }
}