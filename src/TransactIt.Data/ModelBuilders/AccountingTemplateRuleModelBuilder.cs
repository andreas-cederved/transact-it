using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.ModelBuilders
{
    public class AccountingTemplateRuleModelBuilder : IEntityTypeConfiguration<AccountingTemplateRule>
    {
        public void Configure(EntityTypeBuilder<AccountingTemplateRule> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
