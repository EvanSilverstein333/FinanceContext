using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Dto;

namespace FinanceManager.Contract.Commands
{
    public class AddFinancialAccountCommand : ICommand
    {
        public AddFinancialAccountCommand() { } //serializeable
        public AddFinancialAccountCommand(FinancialAccountDto account)
        {
            Account = account;
        }
        public FinancialAccountDto Account { get; set; }
    }
}
