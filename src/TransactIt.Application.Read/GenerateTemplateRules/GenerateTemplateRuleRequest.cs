using MediatR;
using System.Collections.Generic;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Read.GenerateTemplateRules
{
    public class GenerateTemplateRuleRequest : IRequest<List<TransactionTemplateRule>>
    {
        public GenerateTemplateRuleRequest(int transactionId)
        {
            TransactionId = transactionId;
        }

        public int TransactionId { get; }
    }
}