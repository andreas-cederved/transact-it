using FluentValidation;
using System;
using System.Linq;

namespace TransactIt.Application.Write.Transactions
{
    public class SaveTransactionValidator : AbstractValidator<SaveTransactionRequest>
    {
        public SaveTransactionValidator()
        {
            RuleFor(x => x.Transaction).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.Transaction.TransactionDate).GreaterThan(DateTime.MinValue);
                RuleFor(x => x.Transaction.AccountingEntries).NotEmpty();
                RuleForEach(x => x.Transaction.AccountingEntries)
                    .Must(x => x.AccountId > 0)
                    .WithMessage("Account entries must have a AccountId of more than 0");
                RuleFor(x => x.Transaction.AccountingEntries)
                    .Must(x =>
                        x.Sum(item => (item.Side == Domain.Models.AccountingEntry.EntrySide.Debit
                            ? -1 * item.Amount : item.Amount)) == 0)
                    .WithMessage("Total amount of debit and credit must be equal.");
            });

            RuleFor(x => x.LedgerId).GreaterThan(0);
        }
    }
}
