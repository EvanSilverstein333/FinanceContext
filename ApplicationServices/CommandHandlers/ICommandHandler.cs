using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Commands;

namespace ApplicationServices.CommandHandlers
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}
