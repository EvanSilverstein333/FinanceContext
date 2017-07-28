using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Dto;

namespace FinanceManager.Contract.Queries
{
    public class FindFinancialTransactionsByDateQuery: IQuery<FinancialTransactionDto[]>
    {
        public FindFinancialTransactionsByDateQuery() { }
        public FindFinancialTransactionsByDateQuery(DateTime date)
        {
            Date = date;
        }
        public DateTime Date { get; set; }
    }
}
