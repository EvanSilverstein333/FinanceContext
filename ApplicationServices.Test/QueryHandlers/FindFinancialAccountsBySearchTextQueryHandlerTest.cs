using ApplicationServices.QueryHandlers;
using AutoMapper;
using AutoMapper.Configuration;
using Domain.Entities;
using FinanceManager.Contract.Dto;
using FinanceManager.Contract.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Persistance.Repositories;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Test.QueryHandlers
{
    [TestClass]
    public class FindFinancialAccountsBySearchTextQueryHandlerTest
    {
        private FindFinancialAccountsBySearchTextQueryHandler _handler;
        private IUnitOfWork _mockIUnitOfWork;
        private IMapper _mockMapper;
        private FindFinancialAccountsBySearchTextQuery _query;
        private FinancialAccountDto[] _fakeDataDto;

        [TestInitialize]
        public void TestInitialize()
        {
            var mockAccountRepo = Substitute.For<FinancialAccountRepository>();
            _mockIUnitOfWork = Substitute.For<IUnitOfWork>();
            _mockIUnitOfWork.FinancialAccounts.Returns(mockAccountRepo);
            _fakeDataDto = new FinancialAccountDto[2]{
                new FinancialAccountDto(),
                new FinancialAccountDto()
            };
            _mockMapper = Substitute.For<IMapper>();
            _mockMapper.Map<IEnumerable<FinancialAccount>, IEnumerable<FinancialAccountDto>>(Arg.Any<IEnumerable<FinancialAccount>>())
                .Returns(_fakeDataDto);

            _query = new FindFinancialAccountsBySearchTextQuery();
            _handler = new FindFinancialAccountsBySearchTextQueryHandler(_mockIUnitOfWork, _mockMapper);

        }

        [TestMethod]
        public void HandleQuery_ValidInputs()
        {

            var data = _handler.Handle(_query);
            Assert.AreEqual(data.Count(), _fakeDataDto.Count());

        }

    }
}
