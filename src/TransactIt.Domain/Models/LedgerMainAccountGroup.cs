using System;
using System.Collections.Generic;
using System.Text;

namespace TransactIt.Domain.Models
{
    public class LedgerMainAccountGroup
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual IEnumerable<LedgerSubAccountGroup> LedgerSubAccountGroups { get; set; }
    }
}
