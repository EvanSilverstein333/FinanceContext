using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Dto;

namespace FinanceManager.Contract.Commands
{
    public class AddFinancialTransactionCommand : ICommand
    {
        public AddFinancialTransactionCommand() { } //serializeable
        public AddFinancialTransactionCommand(FinancialTransactionDto transaction)
        {
            Transaction = transaction;
        }
        public FinancialTransactionDto Transaction { get; set; }
    }
}
