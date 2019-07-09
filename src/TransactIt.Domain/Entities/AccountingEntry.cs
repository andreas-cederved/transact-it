
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

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public int TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
