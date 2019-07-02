
namespace TransactIt.Domain.Models
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
    }
}
