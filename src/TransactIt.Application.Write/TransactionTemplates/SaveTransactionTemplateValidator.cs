using FluentValidation;
using System.Linq;

namespace TransactIt.Application.Write.TransactionTemplates
{
    public class SaveTransactionTemplateValidator : AbstractValidator<SaveTransactionTemplateRequest>
    {
        public SaveTransactionTemplateValidator()
        {
            RuleFor(x => x.TransactionTemplate).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.TransactionTemplate.Name).NotEmpty();
                RuleFor(x => x.TransactionTemplate.TransactionTemplateRules).NotEmpty();
                RuleForEach(x => x.TransactionTemplate.TransactionTemplateRules)
                    .Must(x => x.LedgerAccountId > 0)
                    .WithMessage("Transaction template rule entries must have a LedgerAccountId of more than 0");
                RuleFor(x => x.TransactionTemplate.TransactionTemplateRules)
                    .Must(x =>
                        x.Sum(item => (item.Side == Domain.Models.AccountingEntry.EntrySide.Debit
                            ? -1 * item.Multiplier : item.Multiplier)) == 0)
                    .WithMessage("Total amount of debit and credit must be equal.");
            });

            RuleFor(x => x.LedgerId).GreaterThan(0);
        }
    }
}
