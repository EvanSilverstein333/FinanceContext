using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Commands;

namespace RequestServices
{
    public interface ICommandFactory
    {
        void Execute<TCommand>(TCommand command) where TCommand : ICommand;

    }
}
