using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Events;

namespace ApplicationServices.EventHandlers
{
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        void Handle(TEvent e);
    }
}
