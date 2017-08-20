using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.UnitOfWork;
using FinanceManager.Contract.Commands;
using FinanceManager.Contract.Events;

namespace ApplicationServices.CommandHandlers
{
    public class RemoveFinancialTransactionCommandHandler : ICommandHandler<RemoveFinancialTransactionCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IDomainEventStore _eventStore;

        public RemoveFinancialTransactionCommandHandler(IUnitOfWork unitOfWork, IDomainEventStore eventStore)
        {
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }
        public void Execute(RemoveFinancialTransactionCommand command)
        {
            var Transaction = _unitOfWork.FinancialTransactions.Get(command.TransactionId);
            _unitOfWork.FinancialTransactions.Remove(Transaction);
            _eventStore.AddToEventQueue(new FinancialTransactionRemovedEvent(command.TransactionId, command.AccountId));
        }
    }
}
