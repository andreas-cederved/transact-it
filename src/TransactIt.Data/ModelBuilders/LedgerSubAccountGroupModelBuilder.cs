using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.ModelBuilders
{
    public class LedgerSubAccountGroupModelBuilder : IEntityTypeConfiguration<LedgerSubAccountGroup>
    {
        public void Configure(EntityTypeBuilder<LedgerSubAccountGroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("getutcdate()");

            builder.HasIndex(x => new { x.LedgerMainAccountGroupId, x.Number })
                .IsUnique();

            builder.HasMany(x => x.LedgerAccounts)
                .WithOne(x => x.LedgerSubAccountGroup)
                .HasForeignKey(x => x.LedgerSubAccountGroupId);
        }
    }
}
