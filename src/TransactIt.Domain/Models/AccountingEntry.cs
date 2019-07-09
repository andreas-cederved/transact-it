
namespace TransactIt.Domain.Models
{
    public class AccountingEntry
    {
        public enum EntrySide
        {
            Debit,
            Credit
        }

        public EntrySide Side { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
    }
}
