using MediatR;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Write.LedgerSubAccountGroups
{
    public class SaveLedgerSubAccountGroupRequest : IRequest
    {
        public SaveLedgerSubAccountGroupRequest(
            int ledgerId,
            int ledgerMainAccountGroupId,
            LedgerSubAccountGroup ledgerSubAccountGroup)
        {
            LedgerId = ledgerId;
            LedgerMainAccountGroupId = ledgerMainAccountGroupId;
            LedgerSubAccountGroup = ledgerSubAccountGroup;
        }

        public int LedgerId { get; }
        public int LedgerMainAccountGroupId { get; }
        public LedgerSubAccountGroup LedgerSubAccountGroup { get; }
    }
}
