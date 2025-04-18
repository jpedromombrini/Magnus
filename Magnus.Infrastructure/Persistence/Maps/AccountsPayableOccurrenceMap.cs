using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class AccountsPayableOccurrenceMap: IEntityTypeConfiguration<AccountsPayableOccurrence>
{
    public void Configure(EntityTypeBuilder<AccountsPayableOccurrence> builder)
    {
        builder.ToTable("AccountsPayableOccurrence");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserId)
            .IsRequired();            
        builder.Property(x => x.UserName)
            .IsRequired()
            .HasColumnType("varchar(100)");
        builder.Property(x => x.Occurrence)
            .IsRequired()
            .HasColumnType("varchar(500)");
    }
}