using FluentValidation;

namespace TransactIt.Application.Read.GenerateTemplateRules
{
    public class GenerateTemplateRuleValidator : AbstractValidator<GenerateTemplateRuleRequest>
    {
        public GenerateTemplateRuleValidator()
        {
            RuleFor(x => x.TransactionId).GreaterThan(0);
        }
    }
}
