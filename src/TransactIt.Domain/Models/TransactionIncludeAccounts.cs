using System;
using System.Collections.Generic;

namespace TransactIt.Domain.Models
{
    public class TransactionIncludeAccounts
    {
        public int Id { get; set; }
        public int IdentifyingCode { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual IEnumerable<AccountingEntryIncludeAccount> AccountingEntries { get; set; }
    }
}
