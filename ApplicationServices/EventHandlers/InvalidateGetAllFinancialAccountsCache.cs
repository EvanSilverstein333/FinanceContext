using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using FinanceManager.Contract.Queries;
using FinanceManager.Contract.Events;

namespace ApplicationServices.EventHandlers
{
    public class InvalidateGetAllFinancialAccountsCache : IDomainEventHandler<FinancialAccountChangedEvent>, IDomainEventHandler<FinancialAccountAddedEvent>, IDomainEventHandler<FinancialAccountRemovedEvent>
    {

        private ObjectCache _cache;

        public InvalidateGetAllFinancialAccountsCache(ObjectCache cache)
        {
            _cache = cache;
        }

        public void Handle(FinancialAccountRemovedEvent e)
        {
            InvalidateCache();
        }

        public void Handle(FinancialAccountAddedEvent e)
        {
            InvalidateCache();
        }

        public void Handle(FinancialAccountChangedEvent e)
        {
            InvalidateCache();
        }

        private void InvalidateCache()
        {
            var queryType = typeof(GetAllFinancialAccountsQuery);
            var key = queryType.Name;
            if (_cache.Contains(key)) { _cache.Remove(key); }
            

        }
    }
}
