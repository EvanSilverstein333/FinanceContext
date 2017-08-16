using System.ServiceProcess;
using System.ServiceModel;
using Infrastructure.Configurations.Services;
using RabbitMQ.Client;

namespace Infrastructure.ServerHosts.WindowsService
{
    public partial class FinanceManagerWindowsService : ServiceBase
    {
        private ServiceHost _commandServiceHost;
        private ServiceHost _queryServiceHost;
        private IConnection _msgPublisherRabbitMQ;

        public FinanceManagerWindowsService()
        {
            InitializeComponent();
            _commandServiceHost = CommandProcessorServiceConfig.Host;
            _queryServiceHost = QueryProcessorServiceConfig.Host;
            _msgPublisherRabbitMQ = Bootstrapper.Container.GetInstance<IConnection>(); // already open
            

        }

        protected override void OnStart(string[] args)
        {
            _commandServiceHost.Open();
            _queryServiceHost.Open();            
            
        }

        protected override void OnStop()
        {
            if(_commandServiceHost != null) { _commandServiceHost.Close(); }
            if (_queryServiceHost != null) { _commandServiceHost.Close(); }
            if (_msgPublisherRabbitMQ!=null) { _msgPublisherRabbitMQ.Close(); }


        }
    }
}
