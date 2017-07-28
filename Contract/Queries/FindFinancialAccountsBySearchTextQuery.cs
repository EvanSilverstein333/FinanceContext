using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Dto;

namespace FinanceManager.Contract.Queries
{
    public class FindFinancialAccountsBySearchTextQuery: IQuery<FinancialAccountDto[]>
    {
        public FindFinancialAccountsBySearchTextQuery() { }
        public FindFinancialAccountsBySearchTextQuery(string searchText)
        {
            SearchText = searchText;
        }
        public string SearchText { get; set; }
    }
}
