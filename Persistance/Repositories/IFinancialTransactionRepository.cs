using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public interface IFinancialTransactionRepository : IRepository<FinancialTransaction,Guid>
    {
        FinancialTransaction[] GetByAccountId(Guid accountId);
    }
}
