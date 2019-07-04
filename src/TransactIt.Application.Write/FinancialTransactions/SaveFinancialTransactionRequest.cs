using MediatR;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Write.FinancialTransactions
{
    public class SaveFinancialTransactionRequest : IRequest
    {
        public SaveFinancialTransactionRequest(int ledgerId, FinancialTransaction financialTransaction)
        {
            LedgerId = ledgerId;
            FinancialTransaction = financialTransaction;
        }

        public int LedgerId { get; }
        public FinancialTransaction FinancialTransaction { get; }
    }
}