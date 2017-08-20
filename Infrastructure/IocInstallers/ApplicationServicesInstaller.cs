using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using ApplicationServices.CommandHandlers;
using ApplicationServices.QueryHandlers;
using ApplicationServices.CrossCuttingConcerns;
using ApplicationServices.EventHandlers;
using Infrastructure.Abstractions;

namespace Infrastructure.IocInstallers
{
    static class ApplicationServicesInstaller
    {

        public static void RegisterServices(Container _simpleContainer)
        {
            _simpleContainer.Register(typeof(ICommandHandler<>), AppDomain.CurrentDomain.GetAssemblies(), Lifestyle.Transient);
            _simpleContainer.Register(typeof(IQueryHandler<,>), AppDomain.CurrentDomain.GetAssemblies(), Lifestyle.Transient);


            // CommandDecorators
            _simpleContainer.RegisterDecorator(typeof(ICommandHandler<>), typeof(CommitTransactionCommandHandlerDecorator<>));
            _simpleContainer.RegisterDecorator(typeof(ICommandHandler<>), typeof(TransactionScopeCommandHandlerDecorator<>));
            _simpleContainer.RegisterDecorator(typeof(ICommandHandler<>), typeof(PerformanceMetricsCommandHandlerDecorator<>));
            //_simpleContainer.RegisterDecorator(typeof(ICommandHandler<>), typeof(ValidationCommandHandlerDecorator<>));
            _simpleContainer.RegisterDecorator(typeof(ICommandHandler<>), typeof(NotifyOnRequestCompletedCommandHandlerDecorator<>));
            _simpleContainer.RegisterDecorator(typeof(ICommandHandler<>), typeof(LoggingCommandHandlerDecorator<>));
            _simpleContainer.RegisterDecorator(typeof(ICommandHandler<>), typeof(ToWcfFaultTranslatorCommandHandlerDecorator<>));


            //Query Decorators
            _simpleContainer.RegisterDecorator(typeof(IQueryHandler<,>), typeof(TransactionScopeQueryHandlerDecorator<,>));
            _simpleContainer.RegisterDecorator(typeof(IQueryHandler<,>), typeof(CachingQueryHandlerDecorator<,>));


            //Events
            _simpleContainer.RegisterCollection(typeof(IDomainEventHandler<>), AppDomain.CurrentDomain.GetAssemblies());
            _simpleContainer.RegisterSingleton<DomainEventStoreImpl>();
            _simpleContainer.RegisterSingleton<IDomainEventStore>(() => _simpleContainer.GetInstance<DomainEventStoreImpl>());
            _simpleContainer.Register<IDomainEventProcessor, DomainEventProcessor>();
            _simpleContainer.Register<IExternalMessagePublisher, ExternalMessagePublisher>();


        }
    }
}
