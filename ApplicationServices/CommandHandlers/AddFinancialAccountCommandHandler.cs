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
    public class AddFinancialAccountCommandHandler : ICommandHandler<AddFinancialAccountCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IDomainEventStore _eventStore;

        public AddFinancialAccountCommandHandler(IUnitOfWork unitOfWork, IDomainEventStore eventStore)
        {
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }
        public void Execute(AddFinancialAccountCommand command)
        {
            var accountDto = command.Account;
            var account = new FinancialAccount(accountDto.Id);
            account.ChangeName(accountDto.FirstName, accountDto.LastName, null);
            _unitOfWork.FinancialAccounts.Add(account);
            _eventStore.AddToEventQueue(new FinancialAccountAddedEvent(accountDto.Id));
            _unitOfWork.Complete();

        }
    }
}
