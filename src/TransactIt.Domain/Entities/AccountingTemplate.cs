using System;
using System.Collections.Generic;

namespace TransactIt.Domain.Entities
{
    public class AccountingTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DefaultFinancialTransactionDescription { get; set; }
        public DateTime CreatedDate { get; set; }

        public int LedgerId { get; set; }
        public virtual Ledger Ledger { get; set; }

        public virtual IEnumerable<AccountingTemplateRule> AccountingTemplateRules { get; set; }
    }
}
