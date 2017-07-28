using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.CrossCuttingConcerns;
using FinanceManager.Contract.Events;
using SimpleInjector;
using ApplicationServices.EventHandlers;
using PublisherSubscriberService;

namespace Infrastructure.Abstractions
{
 
    public class DomainEventProcessor : IDomainEventProcessor
    {
        private Container _container;

        public DomainEventProcessor(Container container)
        {
            _container = container;
        }

        public void Process<TEvent>(TEvent e) where TEvent : IDomainEvent
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(e.GetType());
            dynamic handlers = _container.GetAllInstances(handlerType);
            foreach (var handler in handlers)
            {
                handler.Handle((dynamic)e);

            }

        }
    }
}
