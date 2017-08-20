using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Data.Entity;

namespace Persistance.Repositories
{
    public class FinancialAccountRepository:Repository<FinancialAccount,Guid>
    {
        //public FinancialAccountRepository(DbContext context) : base(context)
        //{
        //}

        public FinancialAccount GetWithTransactions(Guid accountId)
        {
            return _context.Set<FinancialAccount>()
                .Include(x => x.Transactions)
                .Where(x => x.Id == accountId).FirstOrDefault();
        }
    }
}
