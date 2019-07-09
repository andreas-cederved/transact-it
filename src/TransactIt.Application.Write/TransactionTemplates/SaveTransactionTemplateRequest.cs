using MediatR;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Write.TransactionTemplates
{
    public class SaveTransactionTemplateRequest : IRequest
    {
        public SaveTransactionTemplateRequest(int ledgerId, TransactionTemplate transactionTemplate)
        {
            LedgerId = ledgerId;
            TransactionTemplate = transactionTemplate;
        }

        public int LedgerId { get; }
        public TransactionTemplate TransactionTemplate { get; }
    }
}