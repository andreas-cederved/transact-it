using MediatR;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Write.LedgerMainAccountGroups
{
    public class SaveLedgerMainAccountGroupRequest : IRequest
    {
        public SaveLedgerMainAccountGroupRequest(int ledgerId, LedgerMainAccountGroup ledgerMainAccountGroup)
        {
            LedgerId = ledgerId;
            LedgerMainAccountGroup = ledgerMainAccountGroup;
        }

        public int LedgerId { get; }
        public LedgerMainAccountGroup LedgerMainAccountGroup { get; }
    }
}
