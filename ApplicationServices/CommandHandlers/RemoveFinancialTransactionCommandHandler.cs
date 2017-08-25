using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.UnitOfWork;
using FinanceManager.Contract.Commands;
using FinanceManager.Contract.Events;
using Domain.Entities;
using FinanceManager.Contract.Dto;

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
            var transactionDto = command.Transaction;
            var transaction = new FinancialTransaction(transactionDto.Id, transactionDto.AccountId) { RowVersion = transactionDto.RowVersion};
            _unitOfWork.FinancialTransactions.Remove(transaction);
            _eventStore.AddToEventQueue(new FinancialTransactionRemovedEvent(transaction.Id, transaction.AccountId));
        }
    }
}
