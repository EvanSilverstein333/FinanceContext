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
    public class InvalidateGetFinancialAccountByIdCache : IDomainEventHandler<FinancialAccountChangedEvent>, IDomainEventHandler<FinancialAccountRemovedEvent>
    {
        private ObjectCache _cache;

        public InvalidateGetFinancialAccountByIdCache(ObjectCache cache)
        {
            _cache = cache;
        }

        public void Handle(FinancialAccountRemovedEvent e)
        {
            InvalidateCache(e.AccountId);

        }

        public void Handle(FinancialAccountChangedEvent e)
        {
            InvalidateCache(e.AccountId);
        }

        private void InvalidateCache(Guid accountId)
        {
            var queryType = typeof(GetFinancialAccountByIdQuery);
            var key = queryType.Name + accountId;
            if (_cache.Contains(key)) { _cache.Remove(key); }


        }
    }
}
