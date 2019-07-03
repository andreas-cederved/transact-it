using FluentValidation;

namespace TransactIt.Application.Read.Ledgers
{
    public class FindLedgerByIdValidator : AbstractValidator<FindLedgerByIdRequest>
    {
        public FindLedgerByIdValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
