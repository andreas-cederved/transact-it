using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.ModelBuilders
{
    public class AccountingEntryModelBuilder : IEntityTypeConfiguration<AccountingEntry>
    {
        public void Configure(EntityTypeBuilder<AccountingEntry> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Side)
                .HasConversion<int>();

            builder.Property(x => x.Amount)
                .HasColumnType("Money");
        }
    }
}
