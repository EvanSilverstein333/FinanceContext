using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using SimpleInjector.Integration.Wcf;
using Infrastructure.Abstractions;
using System.Data.Entity;
using AutoMapper;
using Infrastructure.Configuration.Mappers;
using log4net;
using ApplicationServices.CrossCuttingConcerns;
using System.Runtime.Caching;
using RabbitMQ.Client;
using Infrastructure.Configurations.Services;
using Infrastructure.ServerHosts.WindowsService;
using SimpleInjector.Diagnostics;
//using FinanceManager.Contract.Services;


namespace Infrastructure.IocInstallers
{
    static class InfrastructureInstaller
    {
        private static Container _container;
        public static void RegisterServices(Container _simpleContainer)
        {
            _container = _simpleContainer;
            _simpleContainer.RegisterSingleton<MapperConfiguration>(AutoMapperConfiguration.MapConfig);
            _simpleContainer.Register<IMapper>(() => _simpleContainer.GetInstance<MapperConfiguration>().CreateMapper());
            _simpleContainer.RegisterSingleton<ObjectCache>(new MemoryCache("MyCache"));
            _simpleContainer.RegisterSingleton<ILog>(Bootstrapper.Logger);
            _simpleContainer.Register<ILogger, Logger>();
            _simpleContainer.Register<ITransactionScope, TransactionScope>();

            _simpleContainer.RegisterSingleton<IConnection>(MsgPublisherRabbitMQConfig.Connection);
            _simpleContainer.RegisterSingleton(new FinanceManagerCommandProcessor());
            _simpleContainer.RegisterSingleton(new FinanceManagerQueryProcessor());
        }

        public static void SuppressWarnings()
        {
            Registration reg = null;

            reg = _container.GetRegistration(typeof(ITransactionScope)).Registration;
            reg.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent,
                "The TransactionScope always gets disposed after commandhandler has run");
        }

    }
}
