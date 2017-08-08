using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FinanceManager.Contract.Commands;
using ApplicationServices.CommandHandlers;
using System.Data;
using System.Diagnostics;

namespace ApplicationServices.CrossCuttingConcerns
{
    public class PerformanceMetricsCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private ICommandHandler<T> _decorated;
        private ILogger _logger;
        public PerformanceMetricsCommandHandlerDecorator(ICommandHandler<T> cmdHandler, ILogger logger)
        {
            _decorated = cmdHandler;
            _logger = logger;
        }
        
        public void Execute(T command)
        {
            var stopWatch = new Stopwatch();
            try
            {
                stopWatch.Start();
                _decorated.Execute(command);
                stopWatch.Stop();
                _logger.Info(string.Format("Time to complete = {0} milliseconds", stopWatch.ElapsedMilliseconds));

            }
            catch
            {
                throw;
            }
        }

    }
}
