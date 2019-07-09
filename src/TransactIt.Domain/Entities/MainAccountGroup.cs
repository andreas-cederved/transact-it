using System;
using System.Collections.Generic;
using System.Text;

namespace TransactIt.Domain.Entities
{
    public class MainAccountGroup
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public int LedgerId { get; set; }
        public virtual Ledger Ledger { get; set; }

        public virtual IEnumerable<SubAccountGroup> SubAccountGroups { get; set; }
    }
}
