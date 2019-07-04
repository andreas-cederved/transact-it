using FluentValidation;

namespace TransactIt.Application.Write.LedgerAccounts
{
    public class SaveLedgerAccountValidator : AbstractValidator<SaveLedgerAccountRequest>
    {
        public SaveLedgerAccountValidator()
        {
            RuleFor(x => x.LedgerAccount).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.LedgerAccount.Number).GreaterThan(0);
                RuleFor(x => x.LedgerAccount.Name).NotEmpty();
            });

            RuleFor(x => x.LedgerSubAccountGroupId).GreaterThan(0);
        }
    }
}
