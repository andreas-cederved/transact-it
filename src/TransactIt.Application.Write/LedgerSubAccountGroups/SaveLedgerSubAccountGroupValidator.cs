using FluentValidation;

namespace TransactIt.Application.Write.LedgerSubAccountGroups
{
    public class SaveLedgerSubAccountGroupValidator : AbstractValidator<SaveLedgerSubAccountGroupRequest>
    {
        public SaveLedgerSubAccountGroupValidator()
        {
            RuleFor(x => x.LedgerSubAccountGroup).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.LedgerSubAccountGroup.Number).GreaterThan(0);
                RuleFor(x => x.LedgerSubAccountGroup.Name).NotEmpty();
            });

            RuleFor(x => x.LedgerId).GreaterThan(0);
            RuleFor(x => x.LedgerMainAccountGroupId).GreaterThan(0);
        }
    }
}
