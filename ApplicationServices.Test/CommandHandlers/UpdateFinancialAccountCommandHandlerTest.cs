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
    public class UpdateFinancialAccountCommandHandlerTest
    {
        private UpdateFinancialAccountCommandHandler _handler;
        private FinancialAccountDto _accountDto;
        private UpdateFinancialAccountCommand _cmd;
        private IUnitOfWork _mockIUOW;
        private IDomainEventStore _mockIEventStore;
        private FinancialAccountRepository _mockRepo;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepo = Substitute.For<FinancialAccountRepository>();
            _mockIUOW = Substitute.For<IUnitOfWork>();
            _mockIUOW.FinancialAccounts.Returns(_mockRepo);
            _mockIEventStore = Substitute.For<IDomainEventStore>();
            _handler = new UpdateFinancialAccountCommandHandler(_mockIUOW, _mockIEventStore);
            _accountDto = new FinancialAccountDto(Guid.NewGuid())
            {
                FirstName = "John",
                LastName = "Smith"
            };
            _cmd = new UpdateFinancialAccountCommand(_accountDto);
        }

        [TestMethod]
        public void ExecuteCommand_WithValidInputs()
        {
            _handler.Execute(_cmd);
            _mockIEventStore.Received().AddToEventQueue(Arg.Is<FinancialAccountChangedEvent>(x => x.AccountId == _accountDto.Id));
            _mockRepo.Received().Update(Arg.Is<FinancialAccount>(
                x => x.FirstName == _accountDto.FirstName &&
                x.LastName == _accountDto.LastName &&
                x.Id == _accountDto.Id
                ));
        }

    }
}
