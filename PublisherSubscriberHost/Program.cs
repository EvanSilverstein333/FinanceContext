using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace PublisherSubscriberHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost publisherHost = new ServiceHost(typeof(PublisherSubscriberService.Publisher)))
            {

                publisherHost.Open();
                var pubAddress = publisherHost.BaseAddresses.SingleOrDefault();
                Console.WriteLine(string.Format(@"publisherHost StartTime: {0}, BaseAddress: {1})", DateTime.Now, pubAddress));


            }
        }
    }
}
