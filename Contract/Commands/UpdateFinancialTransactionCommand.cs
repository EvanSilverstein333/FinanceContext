using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Dto;

namespace FinanceManager.Contract.Commands
{
    public class UpdateFinancialTransactionCommand : ICommand
    {
        public UpdateFinancialTransactionCommand() { }
        public UpdateFinancialTransactionCommand(FinancialTransactionDto transaction)
        {
            Transaction = transaction;
        }
        public FinancialTransactionDto Transaction { get; set; }
    }
}
