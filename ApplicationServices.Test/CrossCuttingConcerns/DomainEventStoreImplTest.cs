using ApplicationServices.CrossCuttingConcerns;
using FinanceManager.Contract.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Test.CrossCuttingConcerns
{
    [TestClass]
    public class DomainEventStoreImplTest
    {
        private DomainEventStoreImpl _eventStore;

        [TestMethod]
        public void AddToEventQueue_ValidInputs()
        {
            var eventStore = new DomainEventStoreImpl();
            var e = new FinancialAccountAddedEvent();
            eventStore.AddToEventQueue(e);
            var events = eventStore.GetEventQueue();

            Assert.AreSame(events[0], e);
        }


        [TestMethod]
        public void ClearEvents_ValidInputs()
        {
            var eventStore = new DomainEventStoreImpl();
            var e = new FinancialAccountAddedEvent();
            eventStore.AddToEventQueue(e);
            Assert.AreNotEqual(eventStore.GetEventQueue().Count(), 0);

            eventStore.ClearEvents();

            Assert.AreEqual(eventStore.GetEventQueue().Count(), 0);
        }

    }
}
