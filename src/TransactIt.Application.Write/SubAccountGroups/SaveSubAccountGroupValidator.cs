using FluentValidation;

namespace TransactIt.Application.Write.SubAccountGroups
{
    public class SaveSubAccountGroupValidator : AbstractValidator<SaveSubAccountGroupRequest>
    {
        public SaveSubAccountGroupValidator()
        {
            RuleFor(x => x.SubAccountGroup).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.SubAccountGroup.Number).GreaterThan(0);
                RuleFor(x => x.SubAccountGroup.Name).NotEmpty();
            });

            RuleFor(x => x.MainAccountGroupId).GreaterThan(0);
        }
    }
}
