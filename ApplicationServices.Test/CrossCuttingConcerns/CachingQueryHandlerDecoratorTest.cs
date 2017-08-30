using ApplicationServices.CommandHandlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Persistance.UnitOfWork;
using FinanceManager.Contract.Commands;
using FinanceManager.Contract.Dto;
using Persistance.Context;
using Persistance.Repositories;
using Domain.Entities;
using FinanceManager.Contract.Events;
using ApplicationServices.QueryHandlers;
using FinanceManager.Contract.Queries;
using System.Runtime.Caching;
using ApplicationServices.CrossCuttingConcerns;
using AutoMapper;

namespace ApplicationServices.Test.CrossCuttingConcerns
{
    [TestClass]
    public class CachingQueryHandlerDecoratorTest
    {
        private ObjectCache _mockCache;
        private CachingQueryHandlerDecorator<GetAllFinancialAccountsQuery, FinancialAccountDto[]> _decorator;
        private IQueryHandler<GetAllFinancialAccountsQuery,FinancialAccountDto[]> _mockDecorated;
        private GetAllFinancialAccountsQuery _query;

        private FinancialAccountDto[] _fakeAccountsData;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockCache = Substitute.For<ObjectCache>();
            _fakeAccountsData = new FinancialAccountDto[0];
            _query = new GetAllFinancialAccountsQuery();
            _mockDecorated = Substitute.For<IQueryHandler<GetAllFinancialAccountsQuery, FinancialAccountDto[]>>();
            _decorator = new CachingQueryHandlerDecorator<GetAllFinancialAccountsQuery, FinancialAccountDto[]>(_mockDecorated, _mockCache);
        }

        [TestMethod]
        public void HandleQuery_AlwaysHitDatabaseWithFindFinancialAccountsBySearchTextQuery()
        {
            var query = new FindFinancialAccountsBySearchTextQuery("Test");
            var mockhandler = Substitute.For<IQueryHandler<FindFinancialAccountsBySearchTextQuery, FinancialAccountDto[]>>();
            mockhandler.Handle(query).Returns(_fakeAccountsData);
            var decorator = new CachingQueryHandlerDecorator<FindFinancialAccountsBySearchTextQuery, FinancialAccountDto[]>(mockhandler, _mockCache);

            var data = decorator.Handle(query);

            _mockCache.DidNotReceive().Get(Arg.Any<string>());
            Assert.AreSame(_fakeAccountsData, data); 
        }

        [TestMethod]
        public void HandleQuery_AlwaysHitDatabaseWithFindFinancialTransactionsByDateQuery()
        {
            var fakeData = new List<FinancialTransactionDto>().ToArray();
            var query = new FindFinancialTransactionsByDateQuery(DateTime.Now);
            var mockhandler = Substitute.For<IQueryHandler<FindFinancialTransactionsByDateQuery, FinancialTransactionDto[]>>();
            mockhandler.Handle(query).Returns(fakeData);
            var decorator = new CachingQueryHandlerDecorator<FindFinancialTransactionsByDateQuery, FinancialTransactionDto[]>(mockhandler, _mockCache);

            var data = decorator.Handle(query);

            _mockCache.DidNotReceive().Get(Arg.Any<string>());
            Assert.AreSame(fakeData, data);
        }

        [TestMethod]
        public void HandleQuery_HitDatabaseWhenNoCachedValue()
        {
            _mockCache.Get(Arg.Any<string>()).Returns(null);
            _mockDecorated.Handle(_query).Returns(_fakeAccountsData);

            var data = _decorator.Handle(_query); // should hit database

            _mockCache.Received().Get(Arg.Any<string>()); // correct path
            Assert.AreSame(_fakeAccountsData, data); // correct outcome 1
            _mockCache.Received().Add(Arg.Any<string>(), _fakeAccountsData, Arg.Any<CacheItemPolicy>()); // correct outcome 2

        }

        [TestMethod]
        public void HandleQuery_ReturnCachedValueIfExists()
        {
            _mockCache.Get(Arg.Any<string>()).Returns(_fakeAccountsData);

            var data = _decorator.Handle(_query); //should return cached value. not hit database

            _mockCache.Received().Get(Arg.Any<string>()); // correct path
            Assert.AreSame(_fakeAccountsData, data);
            _mockCache.DidNotReceive().Add(Arg.Any<string>(), Arg.Any<FinancialAccountDto[]>(), Arg.Any<CacheItemPolicy>()); // correct outcome 2

        }

        [TestMethod]
        public void HandleQuery_DoNotCacheNullValues()
        {
            _fakeAccountsData = null;
            _mockDecorated.Handle(_query).Returns(_fakeAccountsData);

            var data = _decorator.Handle(_query); //should return cached value. not hit database

            _mockCache.Received().Get(Arg.Any<string>()); // correct path
            Assert.AreSame(_fakeAccountsData, data);
            _mockCache.DidNotReceive().Add(Arg.Any<string>(), Arg.Any<FinancialAccountDto[]>(), Arg.Any<CacheItemPolicy>()); // correct outcome 2
        }

    }
}
