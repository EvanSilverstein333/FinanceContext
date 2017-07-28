using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Events;

namespace ApplicationServices.CrossCuttingConcerns
{
    public interface IDomainEventProcessor
    {
        void Process<TEvent>(TEvent e) where TEvent : IDomainEvent;
    }
}
