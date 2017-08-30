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
using FinanceManager.Contract.Events;

namespace ApplicationServices.Test.CrossCuttingConcerns
{
    [TestClass]
    public class NotifyOnRequestCompletedCommandHandlerDecoratorTest
    {
        private NotifyOnRequestCompletedCommandHandlerDecorator<AddFinancialAccountCommand> _decorator;
        private ICommandHandler<AddFinancialAccountCommand> _mockDecorated;
        private DomainEventStoreImpl _eventStore; // cant mock methods on concrete class
        private IDomainEventProcessor _mockEventProcessor;
        private IExternalMessagePublisher _mockExternalPublisher;
        private AddFinancialAccountCommand _command;



        [TestInitialize]
        public void TestInitialize()
        {
            _eventStore = new DomainEventStoreImpl();
            _eventStore.AddToEventQueue(new FinancialAccountAddedEvent());
            _mockDecorated = Substitute.For<ICommandHandler<AddFinancialAccountCommand>>();
            _mockEventProcessor = Substitute.For<IDomainEventProcessor>();
            _mockExternalPublisher = Substitute.For<IExternalMessagePublisher>();
            _command = new AddFinancialAccountCommand();
            _decorator = new NotifyOnRequestCompletedCommandHandlerDecorator<AddFinancialAccountCommand>(_mockDecorated,_eventStore,_mockEventProcessor,_mockExternalPublisher);

        }

        [TestMethod]
        public void ExecuteCommand_DispatchEvents()
        {
            Assert.AreNotEqual(_eventStore.GetEventQueue().Count(), 0); // assert events exist
            _decorator.Execute(_command);
            _mockEventProcessor.Received().Process(Arg.Any<IDomainEvent>());
            _mockExternalPublisher.Received().Publish(Arg.Any<object>());
            Assert.AreEqual(_eventStore.GetEventQueue().Count(), 0); // assert that events are cleared at the end

        }

        [TestMethod]
        public void ExecuteCommand_DoNotDispatchEventsOnError()
        {
            _mockDecorated.When(x => x.Execute(_command)).Do(x => { throw new Exception(""); });
            Assert.AreNotEqual(_eventStore.GetEventQueue().Count(), 0); // assert events exist
            try { _decorator.Execute(_command); }
            catch { };
            _mockEventProcessor.DidNotReceive().Process(Arg.Any<IDomainEvent>());
            _mockExternalPublisher.DidNotReceive().Publish(Arg.Any<object>());
            Assert.AreEqual(_eventStore.GetEventQueue().Count(), 0); // assert that events are cleared at the end

        }

    }
}
