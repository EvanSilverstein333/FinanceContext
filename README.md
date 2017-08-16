# FinanceContext #

This project is a generic bounded context for managing financial information, which can easily be incorporated into any desktop or web application. The project supports CRUD operations for the following data: 
* Financial account - name
* Financial transaction - date, money(amount/currency), transactionType(debit/credit), note

### Features ###

The project supports the following features:
* Logging
* Caching
* Performance metrics
* Optimistic concurrency

### Getting Started ###

This project uses 3 services to enable communication with a given client application. Request messages are issued through 2 WCF services, which are the FinanceManagerCommandService and FinanceManagerQueryService (located in [Abstractions directory](https://github.com/EvanSilverstein333/FinanceContext/tree/master/Infrastructure/Abstractions)). RabbitMQ is used to send notifications to clients through a message broker when domain events occur. The 3 services are listed below. 
* FinanceManagerCommandService - used for operations that change state (create, update, delete)
* FinanceManagerQueryService - used for reading operations
* Message broker - a RabbitMQ message broker with direct exchange for topic-based notifications

Communcation can be achieved with the following steps:

#### Step 1) - Configure services ####
Configuration for each of these services is located in [Services directory](https://github.com/EvanSilverstein333/FinanceContext/tree/master/Infrastructure/Configurations/Services), each of which can be customized to meet your requirements. Proceed to the next step when you are satisfied with your configuration.

#### Step 2) - Add contract assembly to your application
The contract assembly contains the components for communicating with the services. The contents of this assembly are located in [Contract directory](https://github.com/EvanSilverstein333/FinanceContext/tree/master/Contract).

#### Step 3) - Add services to your application ####
The services can be added to your application based on the configuration from Step 1. 

### Examples ###
After completing the steps in the Getting Started section you are ready to use this project in your own application. An example of using each service is provided below.

#### FinanceManagerCommandService ####
The components available to use with this service are provided in the [Commands directory](https://github.com/EvanSilverstein333/FinanceContext/tree/master/Contract/Commands) of the Contract Assembly. For example (assuming a standard MVC client application):

```
public class FinancialAccountController : Controller
{
    using FinanceManager.Contract.Dto;
    using FinanceManager.Contract.Commands;
    
    //other stuff
    
    public void UpdateAccount(FinancialAccountDto account)
    {
        var command = new UpdateFinancialAccountCommand(account);
        var service = new FinanceManagerCommandProcessorClient();
        service.Submit(command);
    }
}
```

#### PatientManagerQueryService ####
The components available to use with this service are provided in the [Queries directory](https://github.com/EvanSilverstein333/FinanceContext/tree/master/Contract/Queries) of the Contract Assembly. For example (assuming a standard MVC client application):

```
public class FinancialAccountController : Controller
{
    using FinanceManager.Contract.Dto;
    using FinanceManager.Contract.Queries;
    
    //other stuff
    
    public FinancialAccountDto GetAccount(Guid accountId)
    {
        var query = new GetFinancialAccountByIdQuery(accountId);
        var service = new FinanceManagerQueryProcessorClient();
        var account = service.Submit(query);
        return patient as FinancialAccountDto;
    }
}
```

#### Message broker ####

The message broker service is configured using direct exchange to notify all listeners of a specific event when that event occurs. The components available to use with this service are provided in the [Events directory](https://github.com/EvanSilverstein333/FinanceContext/tree/master/Contract/Events) of the Contract Assembly. For example:
```

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using System.ServiceModel;
using FinanceManager.Contract.Events;
using System.Diagnostics;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;

namespace WebClient.Infrastructure.Abstractions
{
    public class FinanceManagerMessageCallback : IDisposable
    {
        private static IConnection _connection;
        
        static FinanceManagerMessageCallback()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            SubscribeToMessages();
        }

        private static void SubscribeToMessages()
        {

            var channel = _connection.CreateModel();
            channel.ExchangeDeclare(exchange: "direct_events", type: "direct");

            var queueName = channel.QueueDeclare().QueueName;

            foreach (var e in GetEventTypes())
            {
                channel.QueueBind(queue: queueName,
                                    exchange: "direct_events",
                                    routingKey: e.ToString());
            }

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Message_Received;

            channel.BasicConsume(queue: queueName,
                                    autoAck: true,
                                    consumer: consumer);
            
        }

        private static void Message_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            String jsonified = Encoding.UTF8.GetString(e.Body);
            var jsonSerializerSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            var message = JsonConvert.DeserializeObject(jsonified,jsonSerializerSettings);
            
            if(eventType == typeof(FinancialAccountRemovedEvent))
            {
                //do something
            }
            else
            {
                //do something else
            }

        }

        private static Type[] GetEventTypes()
        {
            var eventTypes = new Type[]
            {
                typeof(FinancialAccountRemovedEvent),
                typeof(FinancialAccountChangedEvent)
            };
            return eventTypes;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}

```

### Installation ###
This repo contains a setup project for installation on a local machine. It is currently configured to be installed as a windows service, which will run automatically when the local machine starts up.

### Authors ###

* Evan Silverstein
