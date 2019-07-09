using MediatR;
using System.Collections.Generic;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Read.DistributeAmounts
{
    public class DistributeAmountRequest : IRequest<List<AccountingEntry>>
    {
        public DistributeAmountRequest(int transactionTemplateId, decimal amount)
        {
            TransactionTemplateId = transactionTemplateId;
            Amount = amount;
        }

        public int TransactionTemplateId { get; }
        public decimal Amount { get; }
    }
}