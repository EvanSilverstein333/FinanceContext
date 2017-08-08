using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Entities;


namespace Persistance.Context
{
    public class FinanceContext:DbContext
    {

        public FinanceContext()
        {
            Database.SetInitializer<FinanceContext>(new DropCreateDatabaseIfModelChanges<FinanceContext>());
            //Database.SetInitializer<FinanceContext>(new DropCreateDatabaseAlways<FinanceContext>());

        }

        public DbSet<FinancialAccount> FinancialAccounts { get; set; }
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinancialAccount>().Property(s => s.RowVersion).IsRowVersion();
            modelBuilder.Entity<FinancialTransaction>().Property(s => s.RowVersion).IsRowVersion();

            modelBuilder.Entity<FinancialAccount>()
                .Property(acc => acc.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);

            modelBuilder.Entity<FinancialAccount>()
                .HasMany(a => a.Transactions)
                .WithRequired()
                .HasForeignKey(t => t.AccountId);

            modelBuilder.Entity<FinancialTransaction>()
                .Property(t => t.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);



            base.OnModelCreating(modelBuilder);
        }
    }
}
