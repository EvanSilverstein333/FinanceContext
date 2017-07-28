using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Contract.Events
{
    public class FinancialAccountAddedEvent : IDomainEvent
    {
        public FinancialAccountAddedEvent() { }
        public FinancialAccountAddedEvent(Guid accountId)
        {
            AccountId = accountId;
        }
        public Guid AccountId { get; set; }
    }
}
