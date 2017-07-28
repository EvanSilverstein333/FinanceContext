using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Dto;

namespace FinanceManager.Contract.Queries
{
    public class GetFinancialTransactionByIdQuery : IQuery<FinancialTransactionDto>
    {
        public GetFinancialTransactionByIdQuery() { }
        public GetFinancialTransactionByIdQuery(Guid transactionId)
        {
            TransactionId = transactionId;
        }
        public Guid TransactionId { get; set; }
    }
}
