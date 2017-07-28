using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Dto;
using ValueObjects.Finance;

namespace FinanceManager.Contract.Queries
{
    public class GetFinancialAccountBalanceQuery : IQuery<Money>
    {
        public GetFinancialAccountBalanceQuery() { }
        public GetFinancialAccountBalanceQuery(Guid accountId)
        {
            AccountId = accountId;
        }
        public Guid AccountId { get; set; }
    }
}
