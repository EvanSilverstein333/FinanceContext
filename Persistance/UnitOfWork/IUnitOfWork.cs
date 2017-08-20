using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Entities;
using Persistance.Repositories;

namespace Persistance.UnitOfWork
{
    public interface IUnitOfWork
    {
        FinancialAccountRepository FinancialAccounts { get; }
        FinancialTransactionRepository FinancialTransactions { get; }
    }
}
