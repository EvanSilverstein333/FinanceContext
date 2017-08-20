using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using Infrastructure.IocInstallers;
using FinanceManager.Contract.Commands;
using log4net;
using System.ServiceProcess;
using Infrastructure.ServerHosts.WindowsService;
using SimpleInjector.Lifestyles;

namespace Infrastructure
{
    public static class Bootstrapper
    {
        public static readonly Container Container;
        public static readonly ILog Logger;
        public static readonly Type[] AllContractTypes; //{ get; private set; }
        public static readonly ServiceBase[] WindowsServices;


        static Bootstrapper()
        {
            Logger = log4net.LogManager.GetLogger("RollingFileLogger");
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();
            InfrastructureInstaller.RegisterServices(container);
            ApplicationServicesInstaller.RegisterServices(container);
            PersistanceInstaller.RegisterServices(container);
            InfrastructureInstaller.SuppressWarnings();
            //PersistanceInstaller.SuppressWarnings();

            container.Verify();
            Container = container;

            AllContractTypes = GetContractTypes();
            WindowsServices = GetWindowsServices(); // this needs be after container is verified and set


        }

        private static Type[] GetContractTypes()
        {
            var allTypesInContractAssembly = typeof(ICommand).Assembly.GetExportedTypes();
            var contractTypes = allTypesInContractAssembly.Where(t => t.IsClass).ToArray();
            return contractTypes;
        }

        private static ServiceBase[] GetWindowsServices()
        {
            ServiceBase[] servicesToRun = new ServiceBase[]
               {
                    new FinanceManagerWindowsService()
               };
            return servicesToRun;
        }
    }
}
