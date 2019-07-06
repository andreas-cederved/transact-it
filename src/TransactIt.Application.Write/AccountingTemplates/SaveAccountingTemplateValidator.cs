using FluentValidation;
using System;
using System.Linq;

namespace TransactIt.Application.Write.AccountingTemplates
{
    public class SaveAccountingTemplateValidator : AbstractValidator<SaveAccountingTemplateRequest>
    {
        public SaveAccountingTemplateValidator()
        {
            RuleFor(x => x.AccountingTemplate).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.AccountingTemplate.Name).NotEmpty();
                RuleFor(x => x.AccountingTemplate.AccountingTemplateRules).NotEmpty();
                RuleForEach(x => x.AccountingTemplate.AccountingTemplateRules)
                    .Must(x => x.LedgerAccountId > 0)
                    .WithMessage("Accounting template rule entries must have a LedgerAccountId of more than 0");
                RuleFor(x => x.AccountingTemplate.AccountingTemplateRules)
                    .Must(x =>
                        x.Sum(item => (item.Side == Domain.Models.AccountingEntry.EntrySide.Debit
                            ? -1 * item.Multiplier : item.Multiplier)) == 0)
                    .WithMessage("Total amount of debit and credit must be equal.");
            });

            RuleFor(x => x.LedgerId).GreaterThan(0);
        }
    }
}
