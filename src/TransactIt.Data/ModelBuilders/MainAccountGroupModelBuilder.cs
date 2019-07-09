using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.ModelBuilders
{
    public class MainAccountGroupModelBuilder : IEntityTypeConfiguration<MainAccountGroup>
    {
        public void Configure(EntityTypeBuilder<MainAccountGroup> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("getutcdate()");

            builder.HasIndex(x => new { x.LedgerId, x.Number })
                .IsUnique();

            builder.HasMany(x => x.SubAccountGroups)
                .WithOne(x => x.MainAccountGroup)
                .HasForeignKey(x => x.MainAccountGroupId);
        }
    }
}
