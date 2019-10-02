using System;
using System.Collections.Generic;
using System.Text;

namespace TransactIt.Domain.Models
{
    public class AccountingEntryIncludeAccount
    {
        public enum EntrySide
        {
            Debit,
            Credit
        }

        public EntrySide Side { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
