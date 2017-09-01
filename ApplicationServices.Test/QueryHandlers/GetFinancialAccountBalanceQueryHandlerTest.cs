using ApplicationServices.QueryHandlers;
using AutoMapper;
using AutoMapper.Configuration;
using Domain.Entities;
using FinanceManager.Contract.Dto;
using FinanceManager.Contract.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Persistance.Context;
using Persistance.Repositories;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ValueObjects.Finance;

namespace ApplicationServices.Test.QueryHandlers
{
    [TestClass]
    public class GetFinancialAccountBalanceQueryHandlerTest
    {
        private GetFinancialAccountBalanceQueryHandler _handler;
        private IUnitOfWork _mockIUnitOfWork;
        private IMapper _mockMapper;
        private GetFinancialAccountBalanceQuery _query;
        private FinancialAccount _mockAccount;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockAccount = Substitute.For<FinancialAccount>(Guid.NewGuid());
            var mockIAccountRepo = Substitute.For<IFinancialAccountRepository>();
            mockIAccountRepo.GetWithTransactions(Arg.Any<Guid>()).Returns(_mockAccount);
            _mockIUnitOfWork = Substitute.For<IUnitOfWork>();
            _mockIUnitOfWork.FinancialAccounts.Returns(mockIAccountRepo);
            _mockMapper = Substitute.For<IMapper>();

            _query = new GetFinancialAccountBalanceQuery();
            _handler = new GetFinancialAccountBalanceQueryHandler(_mockIUnitOfWork, _mockMapper);

        }

        [TestMethod]
        public void HandleQuery_ValidInputs()
        {

            var data = _handler.Handle(_query);
            _mockAccount.Received().CalculateBalance();

        }

    }
}
