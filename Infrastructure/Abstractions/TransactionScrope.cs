using ApplicationServices.CrossCuttingConcerns;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace Infrastructure.Abstractions
{
    public class TransactionScope : ITransactionScope
    {
        private Scope _scope;
        public void BeginScope()
        {
            _scope = ThreadScopedLifestyle.BeginScope(Bootstrapper.Container);
        }

        public void Dispose()
        {
            if (_scope != null) { _scope.Dispose(); }
        }
    }
}
