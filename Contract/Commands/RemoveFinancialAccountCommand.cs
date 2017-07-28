using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Dto;

namespace FinanceManager.Contract.Commands
{
    public class RemoveFinancialAccountCommand : ICommand
    {
        public RemoveFinancialAccountCommand() { }
        public RemoveFinancialAccountCommand(Guid accountId)
        {
            AccountId = accountId;
        }
        public Guid AccountId { get; set; }
    }
}
