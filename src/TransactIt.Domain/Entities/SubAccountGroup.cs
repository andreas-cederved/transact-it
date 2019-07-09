using System;
using System.Collections.Generic;

namespace TransactIt.Domain.Entities
{
    public class SubAccountGroup
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public int MainAccountGroupId { get; set; }
        public virtual MainAccountGroup MainAccountGroup { get; set; }

        public virtual IEnumerable<Account> Accounts { get; set; }
    }
}
