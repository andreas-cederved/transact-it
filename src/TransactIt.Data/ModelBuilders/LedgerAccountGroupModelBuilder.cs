using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.ModelBuilders
{
    public class LedgerAccountGroupModelBuilder : IEntityTypeConfiguration<LedgerAccountGroup>
    {
        public void Configure(EntityTypeBuilder<LedgerAccountGroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("getutcdate()");

            builder.HasMany(x => x.LedgerAccounts)
                .WithOne(x => x.LedgerAccountGroup)
                .HasForeignKey(x => x.LedgerAccountGroupId);
        }
    }
}
