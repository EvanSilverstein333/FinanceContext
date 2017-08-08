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
    public class UpdateFinancialAccountCommandHandler : ICommandHandler<UpdateFinancialAccountCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IDomainEventStore _eventStore;

        public UpdateFinancialAccountCommandHandler(IUnitOfWork unitOfWork, IDomainEventStore eventStore)
        {
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }
        public void Execute(UpdateFinancialAccountCommand command)
        {
            var accountDto = command.Account;
            var account = new FinancialAccount(accountDto.Id);
            account.ChangeName(accountDto.FirstName, accountDto.LastName, accountDto.RowVersion);
            _unitOfWork.FinancialAccounts.Update(account);
            _eventStore.AddToEventQueue(new FinancialAccountChangedEvent(accountDto.Id));
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
