﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using FinanceManager.Contract.Events;
using FinanceManager.Contract.Queries;

namespace ApplicationServices.EventHandlers
{
    public class InvalidateGetFinancialAccountBalanceCache : IDomainEventHandler<FinancialTransactionChangedEvent>, IDomainEventHandler<FinancialTransactionAddedEvent>, IDomainEventHandler<FinancialTransactionRemovedEvent>
    {
        private ObjectCache _cache;

        public InvalidateGetFinancialAccountBalanceCache(ObjectCache cache)
        {
            _cache = cache;
        }

        public void Handle(FinancialTransactionRemovedEvent e)
        {
            InvalidateCache(e.AccountId);

        }

        public void Handle(FinancialTransactionAddedEvent e)
        {
            InvalidateCache(e.AccountId);
        }

        public void Handle(FinancialTransactionChangedEvent e)
        {
            InvalidateCache(e.AccountId);
        }

        private void InvalidateCache(Guid accountId)
        {
            var queryType = typeof(GetFinancialAccountBalanceQuery);
            var key = queryType.Name + accountId;
            if (_cache.Contains(key)) { _cache.Remove(key); }


        }
    }
}
