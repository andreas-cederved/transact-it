using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.ModelBuilders
{
    public class AccountingTemplateModelBuilder : IEntityTypeConfiguration<AccountingTemplate>
    {
        public void Configure(EntityTypeBuilder<AccountingTemplate> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.CreatedDate)
                .HasDefaultValueSql("getutcdate()");

            builder.HasMany(x => x.AccountingTemplateRules)
                .WithOne(x => x.AccountingTemplate)
                .HasForeignKey(x => x.AccountingTemplateId);
        }
    }
}
