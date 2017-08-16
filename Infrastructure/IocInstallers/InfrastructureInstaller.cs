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
//using FinanceManager.Contract.Services;


namespace Infrastructure.IocInstallers
{
    static class InfrastructureInstaller
    {
        public static void RegisterServices(Container _simpleContainer)
        {
            _simpleContainer.RegisterSingleton<MapperConfiguration>(AutoMapperConfiguration.MapConfig);
            _simpleContainer.RegisterSingleton<ObjectCache>(new MemoryCache("MyCache"));
            _simpleContainer.RegisterSingleton<ILog>(Bootstrapper.Logger);
            _simpleContainer.Register<ILogger, Logger>();

            _simpleContainer.RegisterSingleton<IConnection>(MsgPublisherRabbitMQConfig.Connection);
            _simpleContainer.RegisterSingleton(new FinanceManagerCommandProcessor());
            _simpleContainer.RegisterSingleton(new FinanceManagerQueryProcessor());
        }
    }
}
