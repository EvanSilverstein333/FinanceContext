using ApplicationServices.CommandHandlers;
using ApplicationServices.CrossCuttingConcerns;
using FinanceManager.Contract.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ApplicationServices.Test.CrossCuttingConcerns
{
    [TestClass]
    public class LoggingCommandHandlerDecoratorTest
    {
        private LoggingCommandHandlerDecorator<AddFinancialAccountCommand> _decorator;
        private ICommandHandler<AddFinancialAccountCommand> _mockDecorated;
        private ILogger _mockLogger;
        private AddFinancialAccountCommand _command;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockDecorated = Substitute.For<ICommandHandler<AddFinancialAccountCommand>>();
            _mockLogger = Substitute.For<ILogger>();
            _command = new AddFinancialAccountCommand();
            _decorator = new LoggingCommandHandlerDecorator<AddFinancialAccountCommand>(_mockDecorated, _mockLogger);
        }

        [TestMethod]
        public void ExecuteCommand_NoErrorsLogged()
        {
            _decorator.Execute(_command);
            _mockLogger.Received().Info(Arg.Any<object>());
            _mockLogger.DidNotReceive().Error(Arg.Any<object>());
        }

        [TestMethod]
        public void ExecuteCommand_LogValidationException()
        {
            _mockDecorated.When(x => x.Execute(_command)).Do(x => { throw new ValidationException(""); });

            try {_decorator.Execute(_command);}
            catch(ValidationException e)
            {
                _mockLogger.Received().Error(Arg.Any<string>(), e);
            }

        }

        [TestMethod]
        public void ExecuteCommand_LogTimeoutException()
        {
            _mockDecorated.When(x => x.Execute(_command)).Do(x => { throw new TimeoutException(""); });

            try { _decorator.Execute(_command); }
            catch (TimeoutException e)
            {
                _mockLogger.Received().Error(Arg.Any<string>(), e);
            }

        }

    }
}
