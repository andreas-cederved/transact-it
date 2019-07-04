using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.ModelBuilders
{
    public class LedgerAccountModelBuilder : IEntityTypeConfiguration<LedgerAccount>
    {
        public void Configure(EntityTypeBuilder<LedgerAccount> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("getutcdate()");

            builder.HasIndex(x => new { x.LedgerSubAccountGroupId, x.Number })
                .IsUnique();

            builder.HasMany(x => x.AccountingEntries)
                .WithOne(x => x.LedgerAccount)
                .HasForeignKey(x => x.LedgerAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
