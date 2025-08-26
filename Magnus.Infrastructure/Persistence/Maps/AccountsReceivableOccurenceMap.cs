using Magnus.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Magnus.Infrastructure.Persistence.Maps;

public class AccountsReceivableOccurenceMap : IEntityTypeConfiguration<AccountsReceivableOccurence>
{
    public void Configure(EntityTypeBuilder<AccountsReceivableOccurence> builder)
    {
        builder.ToTable("AccountsReceivableOccurence");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(x => x.Occurrence)
            .IsRequired()
            .HasColumnType("varchar(500)");
        builder.HasOne(ar => ar.AccountsReceivable)
            .WithMany(ar => ar.AccountsReceivableOccurences)
            .HasForeignKey(ar => ar.AccountsReceivableId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}