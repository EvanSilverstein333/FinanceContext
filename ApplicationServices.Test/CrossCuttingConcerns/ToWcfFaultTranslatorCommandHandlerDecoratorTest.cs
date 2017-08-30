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
    public class ToWcfFaultTranslatorCommandHandlerDecoratorTest
    {
        private ToWcfFaultTranslatorCommandHandlerDecorator<AddFinancialAccountCommand> _decorator;
        private ICommandHandler<AddFinancialAccountCommand> _mockDecorated;
        private AddFinancialAccountCommand _command;



        [TestInitialize]
        public void TestInitialize()
        {
            _mockDecorated = Substitute.For<ICommandHandler<AddFinancialAccountCommand>>();
            _command = new AddFinancialAccountCommand();
            _decorator = new ToWcfFaultTranslatorCommandHandlerDecorator<AddFinancialAccountCommand>(_mockDecorated);

        }


        [TestMethod]
        [ExpectedException(typeof(FaultException<MyValidator>))]
        public void ExecuteCommand_ThrowValidationFaultException()
        {
            _mockDecorated.When(x => x.Execute(_command)).Do(x => { throw new ValidationException(""); });
            _decorator.Execute(_command);

        }

        [TestMethod]
        public void ExecuteCommand_ValidatorFaultExceptionContainsCorrectInfo()
        {
            var failure1 = new { PropertyName = "test1", Error = "er1" };
            var failure2 = new { PropertyName = "test2", Error = "er2" };

            var errors = new ValidationFailure[2] { new ValidationFailure(failure1.PropertyName, failure1.Error), new ValidationFailure(failure2.PropertyName, failure2.Error) };
            _mockDecorated.When(x => x.Execute(_command)).Do(x => { throw new ValidationException(errors); });
            try { _decorator.Execute(_command); }
            catch(FaultException<MyValidator> e)
            {
                var myValidator = e.Detail;
                Assert.AreEqual(myValidator.Errors.Count, 2);

            }

        }



        [TestMethod]
        [ExpectedException(typeof(FaultException<MyConcurrencyIndicator>))]
        public void ExecuteCommand_ThrowConcurrencyFaultException()
        {
            _mockDecorated.When(x => x.Execute(_command)).Do(x => { throw new OptimisticConcurrencyException(""); });
            _decorator.Execute(_command);
        }
    }
}
