using ApplicationServices.EventHandlers;
using Domain.Entities;
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
    public class InvalidateGetFinancialAccountByIdCacheTest
    {
        private InvalidateGetFinancialAccountByIdCache handler;
        private ObjectCache _mockCache;
        private FinancialAccount _account;
        private string _key;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCache = Substitute.For<ObjectCache>();
            _account = new FinancialAccount(Guid.NewGuid());
            _key = typeof(GetFinancialAccountByIdQuery).Name + _account.Id;
            handler = new InvalidateGetFinancialAccountByIdCache(_mockCache);
            _mockCache.Contains(Arg.Any<string>()).Returns(true);
        }

        [TestMethod]
        public void HandleEvent_FinancialAccountRemovedEvent()
        {
            var e = new FinancialAccountRemovedEvent(_account.Id);
            handler.Handle(e);
            _mockCache.Received().Remove(_key);
        }


        [TestMethod]
        public void HandleEvent_FinancialAccountChangedEvent()
        {
            var e = new FinancialAccountChangedEvent(_account.Id);
            handler.Handle(e);
            _mockCache.Received().Remove(_key);
        }

    }
}
