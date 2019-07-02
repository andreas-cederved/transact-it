using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.ModelBuilders
{
    public class FinancialTransactionModelBuilder : IEntityTypeConfiguration<FinancialTransaction>
    {
        public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("getutcdate()");

            builder.HasMany(x => x.AccountingEntries)
                .WithOne(x => x.FinancialTransaction)
                .HasForeignKey(x => x.FinancialTransactionId);
        }
    }
}
