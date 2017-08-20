using ApplicationServices.CommandHandlers;
using FinanceManager.Contract.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.CrossCuttingConcerns
{
    public interface ITransactionScope : IDisposable
    {
        void BeginScope();
    }
}
