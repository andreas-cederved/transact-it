
namespace TransactIt.Domain.Entities
{
    public class AccountingEntry
    {
        public enum EntrySide
        {
            Debit,
            Credit
        }

        public int Id { get; set; }
        public EntrySide Side { get; set; }
        public decimal Amount { get; set; }

        public int LedgerAccountId { get; set; }
        public virtual LedgerAccount LedgerAccount { get; set; }

        public int FinancialTransactionId { get; set; }
        public virtual FinancialTransaction FinancialTransaction { get; set; }
    }
}
