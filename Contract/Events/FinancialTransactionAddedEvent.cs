using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Contract.Events
{
    public class FinancialTransactionAddedEvent : IDomainEvent
    {
        public FinancialTransactionAddedEvent() { }
        public FinancialTransactionAddedEvent(Guid transactionId, Guid accountId)
        {
            TransactionId = transactionId;
            AccountId = accountId;

        }
        public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
    }
}
