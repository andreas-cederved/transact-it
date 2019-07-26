using FluentValidation;

namespace TransactIt.Application.Read.TransactionTemplates
{
    public class FindAllTransactionTemplatesValidator : AbstractValidator<FindAllTransactionTemplatesRequest>
    {
        public FindAllTransactionTemplatesValidator()
        {
            RuleFor(x => x.LedgerId).GreaterThan(0);
        }
    }
}
