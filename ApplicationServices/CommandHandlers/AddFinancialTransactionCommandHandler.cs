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
    public class AddFinancialTransactionCommandHandler : ICommandHandler<AddFinancialTransactionCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IDomainEventStore _eventStore;

        public AddFinancialTransactionCommandHandler(IUnitOfWork unitOfWork, IDomainEventStore eventStore)
        {
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }
        public void Execute(AddFinancialTransactionCommand command)
        {
            var transactionDto = command.Transaction;
            var transaction = new FinancialTransaction(transactionDto.Id, transactionDto.AccountId, transactionDto.Money, transactionDto.Notes, transactionDto.Date, transactionDto.TransactionType);
            _unitOfWork.FinancialTransactions.Add(transaction);
            _eventStore.AddToEventQueue(new FinancialTransactionAddedEvent(transactionDto.Id, transactionDto.AccountId));
            _unitOfWork.Complete();


            //_postCommit.Committed += () =>
            //{
            //    // Set the output property.
            //    _eventPublisher.AddToEventQueue(new NewPatientRegisteredEvent(patient.Id));
            //    command.CommandCompleted(true);
            //};

        }
    }
}
