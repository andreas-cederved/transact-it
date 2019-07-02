using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.Contexts
{
    public class NoTrackingContext : DbContext
    {
        public NoTrackingContext(DbContextOptions<NoTrackingContext> options) : base(options)
        {
        }

        public virtual DbSet<AccountingEntry> AccountingEntries { get; set; }
        public virtual DbSet<FinancialTransaction> FinancialTransactions { get; set; }
        public virtual DbSet<Ledger> Ledgers { get; set; }
        public virtual DbSet<LedgerAccount> LedgerAccounts { get; set; }
        public virtual DbSet<LedgerAccountGroup> LedgerAccountGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(TrackingContext)));
        }
    }
}
