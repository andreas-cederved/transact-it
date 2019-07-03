using FluentValidation;

namespace TransactIt.Application.Write.LedgerAccountGroups
{
    public class SaveLedgerAccountGroupValidator : AbstractValidator<SaveLedgerAccountGroupRequest>
    {
        public SaveLedgerAccountGroupValidator()
        {
            RuleFor(x => x.LedgerAccountGroup).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.LedgerAccountGroup.Number).GreaterThan(0);
                RuleFor(x => x.LedgerAccountGroup.Name).NotEmpty();
            });

            RuleFor(x => x.LedgerId).GreaterThan(0);
        }
    }
}
