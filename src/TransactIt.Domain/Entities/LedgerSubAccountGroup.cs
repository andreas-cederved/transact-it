using System;
using System.Collections.Generic;

namespace TransactIt.Domain.Entities
{
    public class LedgerSubAccountGroup
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public int LedgerMainAccountGroupId { get; set; }
        public virtual LedgerMainAccountGroup LedgerMainAccountGroup { get; set; }

        public virtual IEnumerable<LedgerAccount> LedgerAccounts { get; set; }
    }
}
