using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Context;
using Persistance.Repositories;
using Persistance.UnitOfWork;
using SimpleInjector;
using System.Data.Entity;
using SimpleInjector.Diagnostics;
using SimpleInjector.Lifestyles;

namespace Infrastructure.IocInstallers
{
    static class PersistanceInstaller
    {
        private static Container _container;

        public static void RegisterServices(Container _simpleContainer)
        {
            _container = _simpleContainer;
            _simpleContainer.Register<UnitOfWork>(Lifestyle.Scoped);
            _simpleContainer.Register<IUnitOfWork>(() => _simpleContainer.GetInstance<UnitOfWork>());
            _simpleContainer.Register<FinanceContext>(Lifestyle.Scoped);
            RegisterRepositories();
        }
        
        private static void RegisterRepositories()
        {
            var repoAssembly = typeof(IRepository).Assembly;
            var repoTypes = repoAssembly.GetExportedTypes()
                .Where(t => t.IsAbstract == false && t.GetInterfaces().Contains(typeof(IRepository)));

            foreach (var type in repoTypes)
            {
                _container.Register(type, type, Lifestyle.Scoped); // implementation is directly injected in uow
            }

        }




    }
}
