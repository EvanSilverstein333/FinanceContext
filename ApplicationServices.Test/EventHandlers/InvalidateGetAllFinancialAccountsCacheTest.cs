using ApplicationServices.EventHandlers;
using FinanceManager.Contract.Dto;
using FinanceManager.Contract.Events;
using FinanceManager.Contract.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Test.EventHandlers
{
    [TestClass]
    public class InvalidateGetAllFinancialAccountsCacheTest
    {
        private InvalidateGetAllFinancialAccountsCache handler;
        private ObjectCache _mockCache;
        private Dictionary<string, object> _cachedData;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCache = Substitute.For<ObjectCache>();
            handler = new InvalidateGetAllFinancialAccountsCache(_mockCache);
            _cachedData = new Dictionary<string, object>();
            _cachedData.Add(typeof(GetAllFinancialAccountsQuery).Name, new FinancialAccountDto[0]);
            _mockCache.Contains(Arg.Any<string>()).Returns(true);
        }

        [TestMethod]
        public void HandleEvent_FinancialAccountRemovedEvent()
        {
            var e = new FinancialAccountRemovedEvent();
            handler.Handle(e);
            _mockCache.Received().Remove(typeof(GetAllFinancialAccountsQuery).Name);
        }

        [TestMethod]
        public void HandleEvent_FinancialAccountAddedEvent()
        {
            var e = new FinancialAccountAddedEvent();
            handler.Handle(e);
            _mockCache.Received().Remove(typeof(GetAllFinancialAccountsQuery).Name);
        }

        [TestMethod]
        public void HandleEvent_FinancialAccountChangedEvent()
        {
            var e = new FinancialAccountChangedEvent();
            handler.Handle(e);
            _mockCache.Received().Remove(typeof(GetAllFinancialAccountsQuery).Name);
        }

    }
}
