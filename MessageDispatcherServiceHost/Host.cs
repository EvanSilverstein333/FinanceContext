using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace MessageDispatcherServiceHost
{
    public static class Host
    {
        public static void Configure<TService>(TService service)
        {
            Uri baseAddress = new Uri("net.tcp://localhost:1900");
            using (var host = new ServiceHost(service, baseAddress))
            {

                var binding = new NetTcpBinding();
                //binding.SendTimeout = new TimeSpan(0, 3, 0);//3 min
                ServiceEndpoint publisherEndpoint =
                host.AddServiceEndpoint(typeof(IPublisher), binding, "PublisherSubscriberService");


                host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = false });
                host.AddServiceEndpoint(typeof(IMetadataExchange),
                    MetadataExchangeBindings.CreateMexTcpBinding(), "net.tcp://localhost:1900/mex");

                host.Open();
                //host.Open();
                //var pubAddress = host.BaseAddresses.SingleOrDefault();
                //Console.WriteLine(string.Format(@"publisherHost StartTime: {0}, BaseAddress: {1})", DateTime.Now, pubAddress));
                controller.ApplicationStart();
                host.Close();
            }

        }


    }
}
}
