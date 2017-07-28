using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Contract.Events
{
    public class FinancialTransactionChangedEvent : IDomainEvent
    {
        public FinancialTransactionChangedEvent() { }
        public FinancialTransactionChangedEvent(Guid transactionId, Guid accountId)
        {
            TransactionId = transactionId;
            AccountId = accountId;
        }
        public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
    }
}
