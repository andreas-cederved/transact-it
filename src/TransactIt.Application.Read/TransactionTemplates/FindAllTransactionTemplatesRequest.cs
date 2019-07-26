using MediatR;
using System.Collections.Generic;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Read.TransactionTemplates
{
    public class FindAllTransactionTemplatesRequest : IRequest<List<TransactionTemplate>>
    {
        public FindAllTransactionTemplatesRequest(int ledgerId)
        {
            LedgerId = ledgerId;
        }

        public int LedgerId { get; }
    }
}