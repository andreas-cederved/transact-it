using FluentValidation;

namespace TransactIt.Application.Write.Accounts
{
    public class SaveAccountValidator : AbstractValidator<SaveAccountRequest>
    {
        public SaveAccountValidator()
        {
            RuleFor(x => x.Account).NotNull().DependentRules(() =>
            {
                RuleFor(x => x.Account.Number).GreaterThan(0);
                RuleFor(x => x.Account.Name).NotEmpty();
            });

            RuleFor(x => x.SubAccountGroupId).GreaterThan(0);
        }
    }
}
