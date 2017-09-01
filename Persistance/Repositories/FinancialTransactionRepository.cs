using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Domain.Entities;

namespace Persistance.Repositories
{
    public class FinancialTransactionRepository: Repository<FinancialTransaction,Guid>, IFinancialTransactionRepository
    {
        public FinancialTransaction[] GetByAccountId(Guid accountId)
        {
            return _context.Set<FinancialTransaction>().Where(x => x.AccountId == accountId).ToArray();
        }

    }
}
