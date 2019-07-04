using MediatR;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Write.LedgerAccounts
{
    public class SaveLedgerAccountRequest : IRequest
    {
        public SaveLedgerAccountRequest(int ledgerSubAccountGroupId, LedgerAccount ledgerAccount)
        {
            LedgerSubAccountGroupId = ledgerSubAccountGroupId;
            LedgerAccount = ledgerAccount;
        }

        public int LedgerSubAccountGroupId { get; }
        public LedgerAccount LedgerAccount { get; }
    }
}