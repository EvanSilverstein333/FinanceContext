﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Contract.Events
{
    public class FinancialAccountChangedEvent : IDomainEvent
    {
        public FinancialAccountChangedEvent() { }
        public FinancialAccountChangedEvent(Guid accountId)
        {
            AccountId = accountId;
        }
        public Guid AccountId { get; set; }
    }
}
