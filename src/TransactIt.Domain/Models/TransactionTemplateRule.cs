
namespace TransactIt.Domain.Models
{
    public class TransactionTemplateRule
    {
        public int Id { get; set; }
        public decimal Multiplier { get; set; }
        public AccountingEntry.EntrySide Side { get; set; }

        public int LedgerAccountId { get; set; }
    }
}
