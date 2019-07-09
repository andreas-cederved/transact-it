using FluentValidation;

namespace TransactIt.Application.Read.DistributeAmounts
{
    public class SaveDistributeAmountValidator : AbstractValidator<DistributeAmountRequest>
    {
        public SaveDistributeAmountValidator()
        {
            RuleFor(x => x.TransactionTemplateId).GreaterThan(0);
            RuleFor(x => x.Amount).NotEmpty();
        }
    }
}
