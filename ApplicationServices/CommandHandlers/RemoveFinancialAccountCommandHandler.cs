using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.UnitOfWork;
using FinanceManager.Contract.Commands;
using FinanceManager.Contract.Events;
using Domain.Entities;

namespace ApplicationServices.CommandHandlers
{
    public class RemoveFinancialAccountCommandHandler : ICommandHandler<RemoveFinancialAccountCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IDomainEventStore _eventStore;

        public RemoveFinancialAccountCommandHandler(IUnitOfWork unitOfWork, IDomainEventStore eventStore)
        {
            _unitOfWork = unitOfWork;
            _eventStore = eventStore;
        }
        public void Execute(RemoveFinancialAccountCommand command)
        {
            var account = new FinancialAccount(command.Account.Id) { RowVersion = command.Account.RowVersion };
            _unitOfWork.FinancialAccounts.Remove(account);
            _eventStore.AddToEventQueue(new FinancialAccountRemovedEvent(command.Account.Id));
        }
    }
}
