using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.CrossCuttingConcerns;
using RabbitMQ.Client;
using Newtonsoft.Json;

namespace Infrastructure.Abstractions
{
    public class ExternalMessagePublisher : IExternalMessagePublisher
    {

        public void Publish<TMessage>(TMessage message)
        {

            var connection = Bootstrapper.Container.GetInstance<IConnection>();

            using (var _channel = connection.CreateModel())
            {
                string _exchangeName = "direct_events";
                _channel.ExchangeDeclare(exchange: _exchangeName, type: "direct");
                var jsonSerializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
                var jsonified = JsonConvert.SerializeObject(message,jsonSerializerSettings);
                byte[] eventBuffer = Encoding.UTF8.GetBytes(jsonified);

                _channel.BasicPublish(exchange: _exchangeName,
                                     routingKey: message.GetType().ToString(),
                                     basicProperties: null,
                                     body: eventBuffer);
            }

        }

    }
}
