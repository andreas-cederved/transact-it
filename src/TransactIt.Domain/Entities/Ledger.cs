using System;
using System.Collections.Generic;

namespace TransactIt.Domain.Entities
{
    public class Ledger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual IEnumerable<FinancialTransaction> FinancialTransactions { get; set; }
        public virtual IEnumerable<LedgerMainAccountGroup> LedgerMainAccountGroups { get; set; }
        public virtual IEnumerable<AccountingTemplate> AccountingTemplates { get; set; }
    }
}
