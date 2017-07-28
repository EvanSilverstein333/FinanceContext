using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using FinanceManager.Contract.Events;
using FinanceManager.Contract.Queries;

namespace ApplicationServices.EventHandlers
{
    public class InvalidateGetFinancialTransactionByIdCache : IDomainEventHandler<FinancialTransactionChangedEvent>, IDomainEventHandler<FinancialTransactionRemovedEvent>
    {
        private ObjectCache _cache;

        public InvalidateGetFinancialTransactionByIdCache(ObjectCache cache)
        {
            _cache = cache;
        }

        public void Handle(FinancialTransactionRemovedEvent e)
        {
            InvalidateCache(e.TransactionId);

        }

        public void Handle(FinancialTransactionChangedEvent e)
        {
            InvalidateCache(e.TransactionId);
        }

        private void InvalidateCache(Guid TransactionId)
        {
            var queryType = typeof(GetFinancialTransactionByIdQuery);
            var key = queryType.Name + TransactionId;
            if (_cache.Contains(key)) { _cache.Remove(key); }


        }
    }
}
