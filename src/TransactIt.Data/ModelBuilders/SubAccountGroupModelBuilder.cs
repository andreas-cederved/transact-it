using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.ModelBuilders
{
    public class SubAccountGroupModelBuilder : IEntityTypeConfiguration<SubAccountGroup>
    {
        public void Configure(EntityTypeBuilder<SubAccountGroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("getutcdate()");

            builder.HasIndex(x => new { x.MainAccountGroupId, x.Number })
                .IsUnique();

            builder.HasMany(x => x.Accounts)
                .WithOne(x => x.SubAccountGroup)
                .HasForeignKey(x => x.SubAccountGroupId);
        }
    }
}
