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
using Persistance.Repositories;
using NSubstitute.ExceptionExtensions;

namespace ApplicationServices.Test.CrossCuttingConcerns
{
    [TestClass]
    public class CommitTransactionCommandHandlerDecoratorTest
    {
        private UnitOfWork _mockUOW;
        private CommitTransactionCommandHandlerDecorator<AddFinancialAccountCommand> _decorator;
        private ICommandHandler<AddFinancialAccountCommand> _mockdecorated;
        private AddFinancialAccountCommand _command;




        [TestInitialize]
        public void TestInitialize()
        {

            _mockUOW = Substitute.For<UnitOfWork>(new FinanceContext(), new FinancialAccountRepository(), new FinancialTransactionRepository());
            _mockdecorated = Substitute.For<ICommandHandler<AddFinancialAccountCommand>>();
            _command = new AddFinancialAccountCommand();
            _decorator = new CommitTransactionCommandHandlerDecorator<AddFinancialAccountCommand>(_mockdecorated, _mockUOW);

        }

        [TestMethod]
        public void ExucuteCommand_CommitTransaction()
        {
            _decorator.Execute(_command);
            _mockUOW.Received().Complete();
        }

        [TestMethod]
        public void ExucuteCommand_DoNotCommitTransactionOnError()
        {
            _mockdecorated.When(x => x.Execute(Arg.Any<AddFinancialAccountCommand>())).Do(x=> { throw new Exception(); });
            try { _decorator.Execute(_command); }
            catch{ }

            _mockUOW.DidNotReceive().Complete();
            
        }
    }
}
