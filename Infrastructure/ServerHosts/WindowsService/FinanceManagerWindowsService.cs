using System.ServiceProcess;
using System.ServiceModel;
using Infrastructure.Configurations.WcfServices;

namespace Infrastructure.ServerHosts.WindowsService
{
    public partial class FinanceManagerWindowsService : ServiceBase
    {
        private ServiceHost _msgPublisherServiceHost;
        private ServiceHost _commandServiceHost;
        private ServiceHost _queryServiceHost;

        public ServiceHost MsgPublisherServiceHost { get { return _msgPublisherServiceHost; } }
        public ServiceHost CommandServiceHost { get { return _commandServiceHost; } }
        public ServiceHost QueryProcessorHost { get { return _queryServiceHost; } }

        public FinanceManagerWindowsService()
        {
            InitializeComponent();
            _msgPublisherServiceHost = MsgPublisherServiceConfig.Host;
            _commandServiceHost = CommandProcessorServiceConfig.Host;
            _queryServiceHost = QueryProcessorServiceConfig.Host;

        }

        protected override void OnStart(string[] args)
        {
            _msgPublisherServiceHost.Open();
            _commandServiceHost.Open();
            _queryServiceHost.Open();
        }

        protected override void OnStop()
        {
            if (_msgPublisherServiceHost != null) { _msgPublisherServiceHost.Close(); }
            if(_commandServiceHost != null) { _commandServiceHost.Close(); }
            if (_queryServiceHost != null) { _commandServiceHost.Close(); }

        }
    }
}
