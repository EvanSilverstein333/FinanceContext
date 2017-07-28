using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.CommandHandlers;
using FinanceManager.Contract.Commands;

namespace ApplicationServices.CrossCuttingConcerns
{
    public class NotifyOnRequestIssuedCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {

        private IExternalMessagePublisher _externalPublisher;
        private ICommandHandler<TCommand> _decoratedHandler;

        public NotifyOnRequestIssuedCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IExternalMessagePublisher externalPublisher)
        {
            _externalPublisher = externalPublisher;
            _decoratedHandler = commandHandler;
        }
        public void Execute(TCommand command)
        {
            _externalPublisher.Publish(command);
            _decoratedHandler.Execute(command);
        }
    }
}
