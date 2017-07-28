using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Queries;

namespace ApplicationServices.QueryHandlers
{
    public interface IQueryHandler<TQuery,TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
