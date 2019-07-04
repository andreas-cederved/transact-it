using FluentValidation;
using System;
using System.Linq;

namespace TransactIt.Application.Write.FinancialTransactions
{
    public class SaveFinancialTransactionValidator : AbstractValidator<SaveFinancialTransactionRequest>
    {
        public SaveFinancialTransactionValidator()
        {
            RuleFor(x => x.FinancialTransaction).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.FinancialTransaction.IdentifyingCode).GreaterThan(0);
                RuleFor(x => x.FinancialTransaction.TransactionDate).GreaterThan(DateTime.MinValue);
                RuleFor(x => x.FinancialTransaction.AccountingEntries).NotEmpty();
                RuleForEach(x => x.FinancialTransaction.AccountingEntries)
                    .Must(x => x.LedgerAccountId > 0)
                    .WithMessage("Account entries must have a LedgerAccountId of more than 0");
                RuleFor(x => x.FinancialTransaction.AccountingEntries)
                    .Must(x =>
                        x.Sum(item => (item.Side == Domain.Models.AccountingEntry.EntrySide.Debit
                            ? -1 * item.Amount : item.Amount)) == 0)
                    .WithMessage("Total amount of debit and credit must be equal.");
            });

            RuleFor(x => x.LedgerId).GreaterThan(0);
        }
    }
}
