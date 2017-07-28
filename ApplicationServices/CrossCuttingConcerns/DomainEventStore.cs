using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.CommandHandlers;
using FinanceManager.Contract.Events;

namespace ApplicationServices.CrossCuttingConcerns
{
    public class DomainEventStoreImpl : IDomainEventStore
    {
        private List<IDomainEvent> _events;

        public DomainEventStoreImpl()
        {
            _events = new List<IDomainEvent>();
        }

        public void AddToEventQueue(IDomainEvent e)
        {
            _events.Add(e);
        }



        public IDomainEvent[] GetEventQueue()
        {
            return _events.ToArray();
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}
