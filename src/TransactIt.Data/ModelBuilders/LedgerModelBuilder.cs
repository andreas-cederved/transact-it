using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.ModelBuilders
{
    public class LedgerModelBuilder : IEntityTypeConfiguration<Ledger>
    {
        public void Configure(EntityTypeBuilder<Ledger> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("getutcdate()");

            builder.HasMany(x => x.FinancialTransactions)
                .WithOne(x => x.Ledger)
                .HasForeignKey(x => x.LedgerId);

            builder.HasMany(x => x.LedgerAccountGroups)
                .WithOne(x => x.Ledger)
                .HasForeignKey(x => x.LedgerId);
        }
    }
}
