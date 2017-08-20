using ApplicationServices.CommandHandlers;
using FinanceManager.Contract.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.CrossCuttingConcerns
{
    public class TransactionScopeCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private Func<ICommandHandler<T>> _decoratedFactory; // delays instantiation until scope is defined
        private ITransactionScope _scope;

        public TransactionScopeCommandHandlerDecorator(Func<ICommandHandler<T>> cmdHandlerFactory, ITransactionScope scope)
        {
            _decoratedFactory = cmdHandlerFactory;
            _scope = scope;
        }

        public void Execute(T command)
        {
            using (_scope)
            {
                _scope.BeginScope();
                var handler = _decoratedFactory.Invoke();
                handler.Execute(command);
            }

        }
    }
}
