using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using SimpleInjector;
//using FinanceManager.Contract.Services;
using System.ServiceModel.Description;
using FinanceManager.Contract.Commands;
using Infrastructure.Abstractions;

namespace Infrastructure.Configurations.Services
{
    public static class CommandProcessorServiceConfig //: IDisposable
    {
        private static ServiceHost _host;
        public static ServiceHost Host { get { return _host; } }


        static CommandProcessorServiceConfig()
        {
            Configure(Bootstrapper.Container);
            //ConfigureContractTypes();
        }
        private static void Configure(Container container)
        {
            //ConfigureContractTypes();
            Uri baseAddressLAN = new Uri("net.tcp://localhost:7080");
            Uri baseAddressWeb = new Uri("http://localhost:7085");
            var host = new ServiceHost(typeof(FinanceManagerCommandProcessor), baseAddressLAN, baseAddressWeb);
            var bindingLAN = new NetTcpBinding(); //{ Name = "LAN"};
            var bindingWeb = new BasicHttpBinding(); //{ Name = "Web"};
            host.AddServiceEndpoint(typeof(FinanceManagerCommandProcessor), bindingLAN, typeof(FinanceManagerCommandProcessor).Namespace);
            host.AddServiceEndpoint(typeof(FinanceManagerCommandProcessor), bindingWeb, typeof(FinanceManagerCommandProcessor).Namespace);
            host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

            _host = host;

        }

    }
}
