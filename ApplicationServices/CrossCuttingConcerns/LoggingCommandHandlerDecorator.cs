using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ApplicationServices.CommandHandlers;
using FinanceManager.Contract.Commands;

namespace ApplicationServices.CrossCuttingConcerns
{
    public class LoggingCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private ICommandHandler<T> _decorated;
        private ILogger _logger;
        public LoggingCommandHandlerDecorator(ICommandHandler<T> cmdHandler, ILogger logger)
        {
            _decorated = cmdHandler;
            _logger = logger;
        }
        
        public void Execute(T command)
        {
            try
            {
                _logger.Info(string.Format("{0} executed", command.GetType().Name));
                _decorated.Execute(command);
            }
            catch(ValidationException e)
            { 
                _logger.Error("Command failed due to validation errors", e);
                throw;
            }
            catch(TimeoutException e)
            {
                _logger.Error("Command failed due to a timeout error", e);
                throw;
            }

        }

    }
}
