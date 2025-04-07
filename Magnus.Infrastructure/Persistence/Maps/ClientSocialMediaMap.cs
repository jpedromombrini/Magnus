using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class ClientSocialMediaMap : IEntityTypeConfiguration<ClientSocialMedia>
{
    public void Configure(EntityTypeBuilder<ClientSocialMedia> builder)
    {
        builder.ToTable("ClientSocialMedia");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.Link)
            .IsRequired()
            .HasColumnType("varchar(150)");
        builder.Property(x => x.RowVersion)
            .IsRowVersion();
    }
}