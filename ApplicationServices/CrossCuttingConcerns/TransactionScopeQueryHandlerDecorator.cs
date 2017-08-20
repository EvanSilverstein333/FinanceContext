using ApplicationServices.CommandHandlers;
using FinanceManager.Contract.Queries;
using ApplicationServices.QueryHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.CrossCuttingConcerns
{
    public class TransactionScopeQueryHandlerDecorator<TQuery, TResult> : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private Func<IQueryHandler<TQuery,TResult>> _decoratedFactory; // delays instantiation until scope is defined
        private ITransactionScope _scope;

        public TransactionScopeQueryHandlerDecorator(Func<IQueryHandler<TQuery,TResult>> queryHandlerFactory, ITransactionScope scope)
        {
            _decoratedFactory = queryHandlerFactory;
            _scope = scope;
        }

        public TResult Handle(TQuery query)
        {
            using (_scope)
            {
                _scope.BeginScope();
                var handler = _decoratedFactory.Invoke();
                return handler.Handle(query);
            }
        }
    }
}
