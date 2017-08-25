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
    public class UpdateFinancialTransactionCommandHandlerTest
    {
        private UpdateFinancialTransactionCommandHandler _handler;
        private FinancialAccountDto _accountDto;
        private FinancialTransactionDto _transactionDto;
        private UpdateFinancialTransactionCommand _cmd;
        private IUnitOfWork _mockIUOW;
        private IDomainEventStore _mockIEventStore;
        private FinancialTransactionRepository _mockRepo;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = Substitute.For<FinancialTransactionRepository>();
            _mockIUOW = Substitute.For<IUnitOfWork>();
            _mockIUOW.FinancialTransactions.Returns(_mockRepo);
            _mockIEventStore = Substitute.For<IDomainEventStore>();
            _handler = new UpdateFinancialTransactionCommandHandler(_mockIUOW, _mockIEventStore);
            _accountDto = new FinancialAccountDto(Guid.NewGuid());
            _transactionDto = new FinancialTransactionDto(Guid.NewGuid(), _accountDto.Id)
            {
                Date = DateTime.Now,
                Money = new ValueObjects.Finance.Money(10, "CAN"),
                TransactionType = ValueObjects.Finance.TransactionType.Credit,
                Notes = "Test",
            };
            _cmd = new UpdateFinancialTransactionCommand(_transactionDto);
        }

        [TestMethod]
        public void ExecuteCommand_WithValidInputs()
        {
            _handler.Execute(_cmd);
            _mockIEventStore.Received().AddToEventQueue(Arg.Is<FinancialTransactionChangedEvent>(
                x => x.TransactionId == _transactionDto.Id &&
                x.AccountId == _transactionDto.AccountId
                ));
            _mockRepo.Received().Update(Arg.Is<FinancialTransaction>(
                x => x.Date == _transactionDto.Date &&
                x.Money == _transactionDto.Money &&
                x.TransactionType == _transactionDto.TransactionType &&
                x.Notes == _transactionDto.Notes &&
                x.Id == _transactionDto.Id &&
                x.AccountId == _transactionDto.AccountId
                ));
        }

    }
}
