using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TransactIt.Domain.Entities;

namespace TransactIt.Data.Contexts
{
    public class TrackingContext : DbContext
    {
        public TrackingContext(DbContextOptions<TrackingContext> options) : base(options)
        {
        }

        public virtual DbSet<AccountingEntry> AccountingEntries { get; set; }
        public virtual DbSet<TransactionTemplate> TransactionTemplates { get; set; }
        public virtual DbSet<TransactionTemplateRule> TransactionTemplateRules { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Ledger> Ledgers { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<MainAccountGroup> MainAccountGroups { get; set; }
        public virtual DbSet<SubAccountGroup> SubAccountGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(TrackingContext)));
        }
    }
}
