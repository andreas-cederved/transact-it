using FluentValidation;

namespace TransactIt.Application.Write.MainAccountGroups
{
    public class SaveMainAccountGroupValidator : AbstractValidator<SaveMainAccountGroupRequest>
    {
        public SaveMainAccountGroupValidator()
        {
            RuleFor(x => x.MainAccountGroup).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.MainAccountGroup.Number).GreaterThan(0);
                RuleFor(x => x.MainAccountGroup.Name).NotEmpty();
            });

            RuleFor(x => x.LedgerId).GreaterThan(0);
        }
    }
}
