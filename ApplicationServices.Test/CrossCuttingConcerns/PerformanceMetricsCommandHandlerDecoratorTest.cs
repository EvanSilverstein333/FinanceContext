using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FinanceManager.Contract.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceManager.Contract.Commands;
using ApplicationServices.CrossCuttingConcerns;
using ApplicationServices.CommandHandlers;
using NSubstitute;

namespace ApplicationServices.Test.CrossCuttingConcerns
{
    [TestClass]
    public class PerformanceMetricsCommandHandlerDecoratorTest
    {
        private PerformanceMetricsCommandHandlerDecorator<AddFinancialAccountCommand> _decorator;
        private ICommandHandler<AddFinancialAccountCommand> _mockDecorated;
        private ILogger _mockLogger;
        private AddFinancialAccountCommand _command;



        [TestInitialize]
        public void TestInitialize()
        {
            _mockDecorated = Substitute.For<ICommandHandler<AddFinancialAccountCommand>>();
            _command = new AddFinancialAccountCommand();
            _mockLogger = Substitute.For<ILogger>();
            _decorator = new PerformanceMetricsCommandHandlerDecorator<AddFinancialAccountCommand>(_mockDecorated, _mockLogger);

        }

        [TestMethod]
        public void ExecuteCommand_LogMetrics()
        {
            _decorator.Execute(_command);
            _mockLogger.Received().Info(Arg.Any<object>());
        }

        [TestMethod]
        public void ExecuteCommand_DoNotLogMetricsOnError()
        {
            _mockDecorated.When(x => x.Execute(_command)).Do(x => { throw new Exception(); });
            try { _decorator.Execute(_command); }
            catch { }
            _mockLogger.DidNotReceive().Info(Arg.Any<object>());
        }
    }
}
