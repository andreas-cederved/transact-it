using MediatR;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Write.LedgerAccountGroups
{
    public class SaveLedgerAccountGroupRequest : IRequest
    {
        public SaveLedgerAccountGroupRequest(int ledgerId, LedgerAccountGroup ledgerAccountGroup)
        {
            LedgerId = ledgerId;
            LedgerAccountGroup = ledgerAccountGroup;
        }

        public int LedgerId { get; }
        public LedgerAccountGroup LedgerAccountGroup { get; }
    }
}
