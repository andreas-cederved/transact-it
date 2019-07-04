using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.ModelBuilders
{
    public class LedgerMainAccountGroupModelBuilder : IEntityTypeConfiguration<LedgerMainAccountGroup>
    {
        public void Configure(EntityTypeBuilder<LedgerMainAccountGroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("getutcdate()");

            builder.HasIndex(x => new { x.LedgerId, x.Number })
                .IsUnique();

            builder.HasMany(x => x.LedgerSubAccountGroups)
                .WithOne(x => x.LedgerMainAccountGroup)
                .HasForeignKey(x => x.LedgerMainAccountGroupId);
        }
    }
}
