using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.CommandHandlers;
using FinanceManager.Contract.Commands;

namespace ApplicationServices.CrossCuttingConcerns
{
    public class NotifyOnRequestCompletedCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand: ICommand
    {
        private DomainEventStoreImpl _eventStore;
        private ICommandHandler<TCommand> _decoratedHandler;
        private IDomainEventProcessor _eventProcessor;
        private IExternalMessagePublisher _externalPublisher;

        public NotifyOnRequestCompletedCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, DomainEventStoreImpl eventStore, IDomainEventProcessor eventProcessor, IExternalMessagePublisher externalPublisher)
        {
            _eventStore = eventStore;
            _decoratedHandler = commandHandler;
            _eventProcessor = eventProcessor;
            _externalPublisher = externalPublisher;
        }
        public void Execute(TCommand command)
        {
            try
            {
                _decoratedHandler.Execute(command);
                DispatchEvents();
            }
            catch
            {
                throw;
            }
            finally
            {
                _eventStore.ClearEvents();
            }
        }

        private void DispatchEvents()
        {
            var events = _eventStore.GetEventQueue();
            foreach(var e in events)
            {
                _eventProcessor.Process(e);
                _externalPublisher.Publish(e);
            }
        }
    }
}
