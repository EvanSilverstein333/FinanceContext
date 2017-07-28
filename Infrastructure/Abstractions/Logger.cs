using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.CrossCuttingConcerns;
using log4net;
using SimpleInjector;

namespace Infrastructure.Abstractions
{
    class Logger : ILogger
    {
        private ILog _logger;
        public Logger(ILog logger)
        {
            _logger = logger;
        }
        public void Error(object message)
        {
            _logger.Error(message);
        }

        public void Error(object message, Exception e)
        {
            _logger.Error(message,e);
        }

        public void Info(object message)
        {
            _logger.Info(message);
        }
    }
}
