using ApplicationServices.CommandHandlers;
using FinanceManager.Contract.Commands;
using Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.CrossCuttingConcerns
{
    public class CommitTransactionCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private UnitOfWork _uow;
        private ICommandHandler<T> _decorated;
        public CommitTransactionCommandHandlerDecorator(ICommandHandler<T> decorated, UnitOfWork uow)
        {
            _decorated = decorated;
            _uow = uow;
        }
        public void Execute(T command)
        {
            try
            {
                _decorated.Execute(command);
                _uow.Complete();
            }
            catch
            {
                throw;
            }
        }
    }
}
