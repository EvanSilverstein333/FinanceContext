using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public interface IFinancialAccountRepository : IRepository<FinancialAccount,Guid>
    {
        FinancialAccount GetWithTransactions(Guid accountId);
    }
}
