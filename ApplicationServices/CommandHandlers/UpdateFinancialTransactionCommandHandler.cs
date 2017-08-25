using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using FinanceManager.Contract.Commands;
using Persistance.UnitOfWork;
using ValueObjects;
using FinanceManager.Contract.Events;


namespace ApplicationServices.CommandHandlers
{
    public class UpdateFinancialTransactionCommandHandler : ICommandHandler<UpdateFinancialTransactionCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IDomainEventStore _eventStore;

        public UpdateFinancialTransactionCommandHandler(IUnitOfWork unitOfWork, IDomainEventStore eventStore)
        {
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }
        public void Execute(UpdateFinancialTransactionCommand command)
        {
            var transactionDto = command.Transaction;
            //var transaction = _unitOfWork.FinancialTransactions.Get(transactionDto.Id);
            var transaction = new FinancialTransaction(transactionDto.Id, transactionDto.AccountId);
            transaction.ChangeInfo(transactionDto.Money, transactionDto.Notes, transactionDto.Date, transactionDto.TransactionType, transactionDto.RowVersion);
            _unitOfWork.FinancialTransactions.Update(transaction);
            _eventStore.AddToEventQueue(new FinancialTransactionChangedEvent(transactionDto.Id,transactionDto.AccountId));
        }
    }
}
