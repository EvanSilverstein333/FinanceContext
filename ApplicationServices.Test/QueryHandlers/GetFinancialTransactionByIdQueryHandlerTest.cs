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
    public class GetFinancialTransactionByIdQueryHandlerTest
    {
        private GetFinancialTransactionByIdQueryHandler _handler;
        private IUnitOfWork _mockIUnitOfWork;
        private IMapper _mockMapper;
        private GetFinancialTransactionByIdQuery _query;
        private FinancialTransactionDto _fakeDataDto;

        [TestInitialize]
        public void TestInitialize()
        {
            var mockTransactionRepo = Substitute.For<FinancialTransactionRepository>();
            _mockIUnitOfWork = Substitute.For<IUnitOfWork>();
            _mockIUnitOfWork.FinancialTransactions.Returns(mockTransactionRepo);
            _fakeDataDto = new FinancialTransactionDto();

            _mockMapper = Substitute.For<IMapper>();
            _mockMapper.Map<FinancialTransaction, FinancialTransactionDto>(Arg.Any<FinancialTransaction>())
                .Returns(_fakeDataDto);

            _query = new GetFinancialTransactionByIdQuery();
            _handler = new GetFinancialTransactionByIdQueryHandler(_mockIUnitOfWork, _mockMapper);

        }

        [TestMethod]
        public void HandleQuery_ValidInputs()
        {

            var data = _handler.Handle(_query);
            Assert.AreSame(data, _fakeDataDto);

        }

    }
}
