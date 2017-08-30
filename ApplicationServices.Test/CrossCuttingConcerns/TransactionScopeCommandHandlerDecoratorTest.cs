using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using FinanceManager.Contract.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceManager.Contract.Commands;
using ApplicationServices.CrossCuttingConcerns;
using ApplicationServices.CommandHandlers;
using NSubstitute;
using System.ServiceModel;
using ValueObjects.Wcf;
using System.Data.Entity.Validation;
using System.Data;

namespace ApplicationServices.Test.CrossCuttingConcerns
{
    [TestClass]
    public class TransactionScopeCommandHandlerDecoratorTest
    {
        private TransactionScopeCommandHandlerDecorator<AddFinancialAccountCommand> _decorator;
        private Func<ICommandHandler<AddFinancialAccountCommand>> _mockFuncDecorated;
        private ITransactionScope _mockScope;
        private AddFinancialAccountCommand _command;




        [TestInitialize]
        public void TestInitialize()
        {
            _mockFuncDecorated = Substitute.For<Func<ICommandHandler<AddFinancialAccountCommand>>>();
            _mockScope = Substitute.For<ITransactionScope>();
            _command = new AddFinancialAccountCommand();
            _decorator = new TransactionScopeCommandHandlerDecorator<AddFinancialAccountCommand>(_mockFuncDecorated, _mockScope);

        }


        [TestMethod]
        public void ExecuteCommand_BeginTransactionScrope()
        {
            _decorator.Execute(_command);
            _mockScope.Received().BeginScope();

        }

        [TestMethod]
        public void ExecuteCommand_TransactionScropeDisposed()
        {
            _decorator.Execute(_command);
            _mockScope.Received().Dispose();

        }



    }
}
