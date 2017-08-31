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
    public class InvalidateGetFinancialTransactionByIdCacheTest
    {
        private InvalidateGetFinancialTransactionByIdCache handler;
        private ObjectCache _mockCache;
        private FinancialTransaction _transaction;
        private string _key;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCache = Substitute.For<ObjectCache>();
            _transaction = new FinancialTransaction(Guid.NewGuid(),Guid.NewGuid());
            _key = typeof(GetFinancialTransactionByIdQuery).Name + _transaction.Id;
            handler = new InvalidateGetFinancialTransactionByIdCache(_mockCache);
            _mockCache.Contains(Arg.Any<string>()).Returns(true);
        }

        [TestMethod]
        public void HandleEvent_FinancialTransactionRemovedEvent()
        {
            var e = new FinancialTransactionRemovedEvent(_transaction.Id, _transaction.AccountId);
            handler.Handle(e);
            _mockCache.Received().Remove(_key);
        }


        [TestMethod]
        public void HandleEvent_FinancialTransactionChangedEvent()
        {
            var e = new FinancialTransactionChangedEvent(_transaction.Id,_transaction.AccountId);
            handler.Handle(e);
            _mockCache.Received().Remove(_key);
        }

    }
}
