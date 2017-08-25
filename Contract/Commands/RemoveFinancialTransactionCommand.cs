using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Dto;

namespace FinanceManager.Contract.Commands
{
    public class RemoveFinancialTransactionCommand : ICommand
    {
        public RemoveFinancialTransactionCommand() { }
        public RemoveFinancialTransactionCommand(FinancialTransactionDto transaction)
        {
            Transaction = transaction;
        }
        public FinancialTransactionDto Transaction { get; set; }
    }
}
