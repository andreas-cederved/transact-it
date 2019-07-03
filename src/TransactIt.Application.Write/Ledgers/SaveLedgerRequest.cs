using MediatR;
using TransactIt.Domain.Models;

namespace TransactIt.Application.Write.Ledgers
{
    public class SaveLedgerRequest : IRequest
    {
        public SaveLedgerRequest(Ledger ledger)
        {
            Ledger = ledger;
        }

        public Ledger Ledger { get; set; }
    }
}
