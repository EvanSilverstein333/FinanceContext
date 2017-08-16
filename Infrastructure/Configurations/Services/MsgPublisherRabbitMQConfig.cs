using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Contract.Events;
using Newtonsoft.Json;
using SimpleInjector;

namespace Infrastructure.Configurations.Services
{
    public static class MsgPublisherRabbitMQConfig
    {
        private static IConnection _connection;
        public static IConnection Connection { get { return _connection; } }

        static MsgPublisherRabbitMQConfig()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            _connection = factory.CreateConnection(); // connection is  open

        }

    }
}
