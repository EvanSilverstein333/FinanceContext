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
using System.Data;

namespace Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FinanceContext _context;
        private FinancialAccountRepository _financialAccounts;
        private FinancialTransactionRepository _financialTransactions;

        public FinancialAccountRepository FinancialAccounts { get { return _financialAccounts; } }
        public FinancialTransactionRepository FinancialTransactions { get { return _financialTransactions; } }

        
        public UnitOfWork(FinanceContext context, FinancialAccountRepository accountRepo, FinancialTransactionRepository transactionRepo)
        {
            _context = context;
            _financialAccounts = accountRepo;
            _financialTransactions = transactionRepo;
            SetContext(context);
        }

        private void SetContext(DbContext context)
        {
            _financialAccounts.SetContext(context);
            _financialTransactions.SetContext(context);
        }

        public int Complete()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new OptimisticConcurrencyException("Another user has updated that entry", e);
            }

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
