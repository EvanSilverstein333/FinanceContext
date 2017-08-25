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

namespace ApplicationServices.Test.CommandHandlers
{
    [TestClass]
    public class RemoveFinancialTransactionCommandHandlerTest
    {
        private RemoveFinancialTransactionCommandHandler _handler;
        private FinancialAccountDto _accountDto;
        private FinancialTransactionDto _transactionDto;
        private IEnumerable<FinancialTransactionDto> _TransactionDtoCollection;
        private RemoveFinancialTransactionCommand _cmd;
        private IUnitOfWork _mockIUOW;
        private IDomainEventStore _mockIEventStore;
        private FinancialTransactionRepository _mockRepo;

        [TestInitialize]
        public void TestInitialize()
        {
            _accountDto = new FinancialAccountDto(Guid.NewGuid());
            _transactionDto = new FinancialTransactionDto(Guid.NewGuid(), _accountDto.Id)
            {
                Date = DateTime.Now,
                Money = new ValueObjects.Finance.Money(10, "CAN"),
                TransactionType = ValueObjects.Finance.TransactionType.Credit,
                Notes = "Test",
                RowVersion = new byte[0]
            };


            _TransactionDtoCollection = new List<FinancialTransactionDto>() {
                _transactionDto,
                new FinancialTransactionDto(),
                new FinancialTransactionDto()
            };

            _mockRepo = Substitute.For<FinancialTransactionRepository>();
            _mockIUOW = Substitute.For<IUnitOfWork>();
            _mockIUOW.FinancialTransactions.Returns(_mockRepo);
            _mockIEventStore = Substitute.For<IDomainEventStore>();
            _handler = new RemoveFinancialTransactionCommandHandler(_mockIUOW, _mockIEventStore);
            _cmd = new RemoveFinancialTransactionCommand(_transactionDto);
        }

        [TestMethod]
        public void ExecuteCommand_WithValidInputs()
        {
            _handler.Execute(_cmd);
            _mockIEventStore.Received().AddToEventQueue(Arg.Is<FinancialTransactionRemovedEvent>(x => x.TransactionId == _transactionDto.Id));
            _mockRepo.Received().Remove(Arg.Is<FinancialTransaction>(
                x => x.Id == _transactionDto.Id &&
                x.RowVersion == _transactionDto.RowVersion
                ));
        }

    }
}
