using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.ModelBuilders
{
    public class TransactionTemplateModelBuilder : IEntityTypeConfiguration<TransactionTemplate>
    {
        public void Configure(EntityTypeBuilder<TransactionTemplate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("getutcdate()");

            builder.HasMany(x => x.TransactionTemplateRules)
                .WithOne(x => x.TransactionTemplate)
                .HasForeignKey(x => x.TransactionTemplateId);
        }
    }
}
