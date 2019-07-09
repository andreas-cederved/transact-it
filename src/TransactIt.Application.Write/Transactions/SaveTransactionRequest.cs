using MediatR;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Write.Transactions
{
    public class SaveTransactionRequest : IRequest
    {
        public SaveTransactionRequest(int ledgerId, Transaction transaction)
        {
            LedgerId = ledgerId;
            Transaction = transaction;
        }

        public int LedgerId { get; }
        public Transaction Transaction { get; }
    }
}