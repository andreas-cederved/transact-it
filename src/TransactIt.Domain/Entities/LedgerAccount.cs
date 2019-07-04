﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TransactIt.Domain.Entities
{
    public class LedgerAccount
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public int LedgerSubAccountGroupId { get; set; }
        public virtual LedgerSubAccountGroup LedgerSubAccountGroup { get; set; }

        public virtual IEnumerable<AccountingEntry> AccountingEntries { get; set; }
    }
}
