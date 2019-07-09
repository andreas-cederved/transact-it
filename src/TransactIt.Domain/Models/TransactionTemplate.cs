using System;
using System.Collections.Generic;

namespace TransactIt.Domain.Models
{
    public class TransactionTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DefaultTransactionDescription { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual IEnumerable<TransactionTemplateRule> TransactionTemplateRules { get; set; }
    }
}
