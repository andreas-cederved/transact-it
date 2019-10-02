using FluentValidation;

namespace TransactIt.Application.Read.Transactions
{
    public class FindTransactionByIdValidator : AbstractValidator<FindTransactionByIdRequest>
    {
        public FindTransactionByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
