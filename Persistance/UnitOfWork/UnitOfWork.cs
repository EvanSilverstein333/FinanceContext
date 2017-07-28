using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using Domain.Entities;
using Persistance.Repositories;
using Persistance.Context;

namespace Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FinanceContext _context;
        private FinancialAccountRepository _financialAccounts;
        private FinancialTransactionRepository _financialTransactions;

        public FinancialAccountRepository FinancialAccounts { get { return _financialAccounts; } }
        public FinancialTransactionRepository FinancialTransactions { get { return _financialTransactions; } }

        
        public UnitOfWork(FinanceContext context)
        {
            _context = context;
            RepositoryFactory(context);
        }

        private void RepositoryFactory(DbContext context)
        {
            _financialAccounts = new FinancialAccountRepository(context);
            _financialTransactions = new FinancialTransactionRepository(context);

        }

        public int Complete()
        {
            
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
