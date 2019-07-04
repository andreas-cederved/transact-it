using FluentValidation;

namespace TransactIt.Application.Write.LedgerMainAccountGroups
{
    public class SaveLedgerMainAccountGroupValidator : AbstractValidator<SaveLedgerMainAccountGroupRequest>
    {
        public SaveLedgerMainAccountGroupValidator()
        {
            RuleFor(x => x.LedgerMainAccountGroup).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.LedgerMainAccountGroup.Number).GreaterThan(0);
                RuleFor(x => x.LedgerMainAccountGroup.Name).NotEmpty();
            });

            RuleFor(x => x.LedgerId).GreaterThan(0);
        }
    }
}
