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

namespace Infrastructure.IocInstallers
{
    static class PersistanceInstaller
    {
        private static Container _container;

        public static void RegisterServices(Container _simpleContainer)
        {
            _container = _simpleContainer;
            _simpleContainer.Register(typeof(IRepository<,>), AppDomain.CurrentDomain.GetAssemblies(), Lifestyle.Transient);
            _simpleContainer.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Transient);
            _simpleContainer.Register<DbContext,FinanceContext>(Lifestyle.Transient);
            _simpleContainer.Register<FinanceContext>(Lifestyle.Transient);
        }

        public static void SuppressWarnings()
        {
            Registration reg = null;

            reg = _container.GetRegistration(typeof(IUnitOfWork)).Registration;
            reg.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, // this should be scoped
                "The uow is expected to dispose after commandhandler has run");

            reg = _container.GetRegistration(typeof(FinanceContext)).Registration;
            reg.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, // this should be scoped
                "The context is expected to dispose after commandhandler has run");
        }


    }
}
