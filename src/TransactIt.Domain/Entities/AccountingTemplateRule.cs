
namespace TransactIt.Domain.Entities
{
    public class AccountingTemplateRule
    {
        public int Id { get; set; }
        public decimal Multiplier { get; set; }
        public AccountingEntry.EntrySide Side { get; set; }

        public int LedgerAccountId { get; set; }
        public virtual LedgerAccount LedgerAccount { get; set; }

        public int AccountingTemplateId { get; set; }
        public virtual AccountingTemplate AccountingTemplate { get; set; }
    }
}
