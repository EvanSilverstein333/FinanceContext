using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using Infrastructure.IocInstallers;
using FinanceManager.Contract.Commands;
using log4net;

namespace Infrastructure
{
    public static class Bootstrapper
    {
        public static readonly Container Container;
        public static readonly ILog Logger;
        public static Type[] AllContractTypes { get; private set; }


        static Bootstrapper()
        {
            Logger = log4net.LogManager.GetLogger("RollingFileLogger");
            var container = new Container();
            InfrastructureInstaller.RegisterServices(container);
            ApplicationServicesInstaller.RegisterServices(container);
            PersistanceInstaller.RegisterServices(container);
            PersistanceInstaller.SuppressWarnings();

            container.Verify();
            Container = container;
            GetContractTypes();

        }

        private static void GetContractTypes()
        {
            var allTypesInContractAssembly = typeof(ICommand).Assembly.GetExportedTypes();
            var contractTypes = allTypesInContractAssembly.Where(t => t.IsClass).ToArray();
            AllContractTypes = contractTypes;
        }


        //public static void RegisterServices()
        //{

        //}
        public static T GetInstance<T>() where T :class
        {
            return Container.GetInstance<T>();
        }
        public static object GetInstance(Type type)
        {
            return Container.GetInstance(type);
        }
    }
}
