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
        public RemoveFinancialTransactionCommand(Guid transactionId, Guid accountId)
        {
            TransactionId = transactionId;
            AccountId = accountId;
        }
        public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
    }
}
