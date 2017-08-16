using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using SimpleInjector;
using System.ServiceModel.Description;
using Infrastructure.Abstractions;
//using FinanceManager.Contract.Services;

namespace Infrastructure.Configurations.Services
{
    public static class QueryProcessorServiceConfig //: IDisposable
    {


        static private ServiceHost _host;
        public static ServiceHost Host { get { return _host; } }


        static QueryProcessorServiceConfig()
        {
            Configure(Bootstrapper.Container);
            //ConfigureContractTypes();
        }
        private static void Configure(Container container)
        {
            //ConfigureContractTypes();
            Uri baseAddressLAN = new Uri("net.tcp://localhost:7070");
            Uri baseAddressWeb = new Uri("http://localhost:7075");
            var host = new ServiceHost(typeof(FinanceManagerQueryProcessor), baseAddressLAN, baseAddressWeb);
            var bindingLAN = new NetTcpBinding();// { Name = "LAN" };
            var bindingWeb = new BasicHttpBinding();// { Name = "Web" };
            host.AddServiceEndpoint(typeof(FinanceManagerQueryProcessor), bindingLAN, typeof(FinanceManagerQueryProcessor).Namespace);
            host.AddServiceEndpoint(typeof(FinanceManagerQueryProcessor), bindingWeb, typeof(FinanceManagerQueryProcessor).Namespace);
            host.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            host.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), "mex");
            host.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

            _host = host;

        }

        //public static void Open()
        //{
        //    _host.Open();
        //}

        //public static void Close()
        //{
        //    _host.Close();
        //}

        //public static void Dispose()
        //{
        //    if (_host != null) { _host.Close(); }
        //}

    }
}
