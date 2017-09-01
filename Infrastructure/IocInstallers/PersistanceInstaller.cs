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
            //_simpleContainer.Register(typeof(IRepository<,>), AppDomain.CurrentDomain.GetAssemblies(), Lifestyle.Scoped);

            RegisterRepositories();
        }


        private static void RegisterRepositories()
        {
            var repoAssembly = typeof(IRepository<,>).Assembly;
            var repoRegistrations = repoAssembly.GetExportedTypes()
                .Where(t => t.IsAbstract == false && t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<,>)))
                .Select( t => new {Type = t, Service = t.GetInterfaces().Where(i => i.IsGenericType == false).First()});

            foreach (var reg in repoRegistrations)
            {
                _container.Register(reg.Service, reg.Type, Lifestyle.Scoped); // implementation is directly injected in uow
            }

        }




    }
}
