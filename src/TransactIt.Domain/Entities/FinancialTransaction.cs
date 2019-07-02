    using System;
using System.Collections.Generic;
using System.Text;

namespace TransactIt.Domain.Entities
{
    public class FinancialTransaction
    {
        public int Id { get; set; }
        public int IdentifyingCode { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual IEnumerable<AccountingEntry> AccountingEntries { get; set; }

        public int LedgerId { get; set; }
        public virtual Ledger Ledger { get; set; }
    }
}
