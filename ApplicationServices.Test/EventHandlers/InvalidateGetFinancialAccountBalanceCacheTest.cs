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
    public class InvalidateGetFinancialAccountBalanceCacheTest
    {
        private InvalidateGetFinancialAccountBalanceCache handler;
        private ObjectCache _mockCache;
        private FinancialAccount _account;
        private string _key;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCache = Substitute.For<ObjectCache>();
            _account = new FinancialAccount(Guid.NewGuid());
            _key = typeof(GetFinancialAccountBalanceQuery).Name + _account.Id;
            handler = new InvalidateGetFinancialAccountBalanceCache(_mockCache);
            _mockCache.Contains(Arg.Any<string>()).Returns(true);
        }

        [TestMethod]
        public void HandleEvent_FinancialTransactionRemovedEvent()
        {
            var e = new FinancialTransactionRemovedEvent(Guid.NewGuid(),_account.Id);
            handler.Handle(e);
            _mockCache.Received().Remove(_key);
        }

        [TestMethod]
        public void HandleEvent_TransactionAccountAddedEvent()
        {
            var e = new FinancialTransactionAddedEvent(Guid.NewGuid(), _account.Id);
            handler.Handle(e);
            _mockCache.Received().Remove(_key);
        }

        [TestMethod]
        public void HandleEvent_FinancialTransactionChangedEvent()
        {
            var e = new FinancialTransactionRemovedEvent(Guid.NewGuid(), _account.Id);
            handler.Handle(e);
            _mockCache.Received().Remove(_key);
        }

    }
}
