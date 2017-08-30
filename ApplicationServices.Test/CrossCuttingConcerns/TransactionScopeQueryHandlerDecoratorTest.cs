using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FinanceManager.Contract.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceManager.Contract.Queries;
using ApplicationServices.CrossCuttingConcerns;
using ApplicationServices.QueryHandlers;
using NSubstitute;
using System.ServiceModel;
using ValueObjects.Wcf;
using System.Data.Entity.Validation;
using System.Data;
using FinanceManager.Contract.Dto;

namespace ApplicationServices.Test.CrossCuttingConcerns
{
    [TestClass]
    public class TransactionScopeQueryHandlerDecoratorTest
    {
        private TransactionScopeQueryHandlerDecorator<GetAllFinancialAccountsQuery,FinancialAccountDto[]> _decorator;
        private Func<IQueryHandler<GetAllFinancialAccountsQuery, FinancialAccountDto[]>> _mockFuncDecorated;
        private ITransactionScope _mockScope;
        private GetAllFinancialAccountsQuery _query;
        private FinancialAccountDto[] _fakeAccountsData;




        [TestInitialize]
        public void TestInitialize()
        {
            _fakeAccountsData = new FinancialAccountDto[0];
            _mockFuncDecorated = Substitute.For<Func<IQueryHandler<GetAllFinancialAccountsQuery, FinancialAccountDto[]>>>();
            _mockScope = Substitute.For<ITransactionScope>();
            _query = new GetAllFinancialAccountsQuery();
            _decorator = new TransactionScopeQueryHandlerDecorator<GetAllFinancialAccountsQuery, FinancialAccountDto[]>(_mockFuncDecorated, _mockScope);

        }


        [TestMethod]
        public void HandleQuery_BeginTransactionScrope()
        {
            _mockFuncDecorated.Invoke().Handle(_query).Returns(_fakeAccountsData);
            var data = _decorator.Handle(_query);
            _mockScope.Received().BeginScope();
            Assert.AreSame(data, _fakeAccountsData);

        }

        [TestMethod]
        public void HandleQuery_TransactionScropeDisposed()
        {
            _mockFuncDecorated.Invoke().Handle(_query).Returns(_fakeAccountsData);
            var data = _decorator.Handle(_query);
            _mockScope.Received().Dispose();
            Assert.AreSame(data, _fakeAccountsData);


        }



    }
}
