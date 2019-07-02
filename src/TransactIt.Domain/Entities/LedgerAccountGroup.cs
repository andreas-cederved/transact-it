using System;
using System.Collections.Generic;

namespace TransactIt.Domain.Entities
{
    public class LedgerAccountGroup
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public int LedgerId { get; set; }
        public virtual Ledger Ledger { get; set; }

        public virtual IEnumerable<LedgerAccount> LedgerAccounts { get; set; }
    }
}
