using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Dto;

namespace FinanceManager.Contract.Queries
{
    public class GetFinancialTransactionsByAccountIdQuery : IQuery<FinancialTransactionDto[]>
    {
        public GetFinancialTransactionsByAccountIdQuery() { }
        public GetFinancialTransactionsByAccountIdQuery(Guid accountId)
        {
            AccountId = accountId;
        }
        public Guid AccountId { get; set; }
    }
}
