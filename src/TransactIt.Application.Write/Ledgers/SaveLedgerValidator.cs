using FluentValidation;

namespace TransactIt.Application.Write.Ledgers
{
    public class SaveLedgerValidator : AbstractValidator<SaveLedgerRequest>
    {
        public SaveLedgerValidator()
        {
            RuleFor(x => x.Ledger).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.Ledger.Name).NotEmpty();
            });
        }
    }
}
