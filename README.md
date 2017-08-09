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

This project uses 3 WCF services to enable communication with a given application. The FinanceManagerCommandService and FinanceManagerQueryService are located in [Abstractions directory](https://github.com/EvanSilverstein333/FinanceContext/tree/master/Infrastructure/Abstraction), while an external service is used as a message bus. The 3 services are listed below.  
* FinanceManagerCommandService - used for operations that change state (create, update, delete)
* FinanceManagerQueryService - used for reading operations
* Publisher - a topic-based publisher that notifies listeners of events that occur

Communcation can be achieved with the following steps:

#### Step 1) - Configure services ####
Configuration for each of these services is located in [WcfServices directory](https://github.com/EvanSilverstein333/FinanceContext/tree/master/Infrastructure/Configurations/WcfServices), each of which can be customized to meet your requirements. Proceed to the next step when you are satisfied with your configuration.

#### Step 2) - Add contract assembly to your application
The contract assembly contains the components for communicating with the services. The contents of this assembly are located in [Contract directory](https://github.com/EvanSilverstein333/FinanceContext/tree/master/Contract).

#### Step 3) - Add services to your application ####
Add the services to your application based on the binding configurations from Step 1 (for help on adding services: https://msdn.microsoft.com/en-us/library/cc636424(v=ax.50).aspx).  

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

#### Publisher ####

The Publisher service is configured as a duplex channel with 1-way communication to notify all listeners of a specific event when that event occurs. Each listener may subscribe to 1 or more events. A listener is established by implementing the "IPublisherCallback" contract, which can be found in the namespace created upon adding the Publisher service. The IPublisherCallback contract is defined by the "void MessageHandler(MessageWrapper messageWrapper)" method, which is called each time the publisher notifies a given listener of an event. Each listener must subscribe to events to recieve notifications, which is achieved by calling the "Subscribe(string messageType)" method of the Publisher service. For example:

The components available to use with this service are provided in the [Events directory](https://github.com/EvanSilverstein333/FinanceContext/tree/master/Contract/Events) of the Contract Assembly. For example:
```
public class FinanceManagerMessageCallback : IPublisherCallback
{
    using System.ServiceModel;  //required for InstanceContext (duplex channel) 
    using FinanceManger.Contract.Events; 
    
    
    public static void SubscribeToMessages() //call this at the beginning of your app
    {
        var context = new InstanceContext(this) //for duplex channel
        var publisher = new PublisherClient(context);
        publisher.Subscribe(typeof(FinancialAccountRemovedEvent).ToString());
        publisher.Subscribe(typeof(FinancialAccountChangedEvent).ToString());
    }

    public void MessageHandler(MessageWrapper messageWrapper)
    {

        var eventType = messageWrapper.Message.GetType();
        if(eventType == typeof(FinancialAccountRemovedEvent))
        {
            //do something
        }
        else
        {
            //do something else
        }
    }
}
```

### Installation ###
This repo contains a setup project for installation on a local machine. It is currently configured to be installed as a windows service, which will run automatically when the local machine starts up.

### Authors ###

* Evan Silverstein
