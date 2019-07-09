using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.ModelBuilders
{
    public class TransactionTemplateRuleModelBuilder : IEntityTypeConfiguration<TransactionTemplateRule>
    {
        public void Configure(EntityTypeBuilder<TransactionTemplateRule> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Multiplier)
                .HasColumnType("Money");
        }
    }
}
