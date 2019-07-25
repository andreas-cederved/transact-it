using FluentValidation;

namespace TransactIt.Application.Read.DistributeAmounts
{
    public class DistributeAmountValidator : AbstractValidator<DistributeAmountRequest>
    {
        public DistributeAmountValidator()
        {
            RuleFor(x => x.TransactionTemplateId).GreaterThan(0);
            RuleFor(x => x.Amount).NotEmpty();
        }
    }
}
