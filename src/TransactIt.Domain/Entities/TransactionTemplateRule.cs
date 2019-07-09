
namespace TransactIt.Domain.Entities
{
    public class TransactionTemplateRule
    {
        public int Id { get; set; }
        public decimal Multiplier { get; set; }
        public AccountingEntry.EntrySide Side { get; set; }

        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        public int TransactionTemplateId { get; set; }
        public virtual TransactionTemplate TransactionTemplate { get; set; }
    }
}
