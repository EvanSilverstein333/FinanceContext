using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Dto;

namespace FinanceManager.Contract.Queries
{
    public class GetFinancialAccountByIdQuery : IQuery<FinancialAccountDto>
    {
        public GetFinancialAccountByIdQuery() { }
        public GetFinancialAccountByIdQuery(Guid accountId)
        {
            AccountId = accountId;
        }
        public Guid AccountId { get; set; }
    }
}
