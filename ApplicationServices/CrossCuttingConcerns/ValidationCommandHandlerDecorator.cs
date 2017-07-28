using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ApplicationServices.Validation.Validators;
using FluentValidation;
using FluentValidation.Results;
using FinanceManager.Contract.Commands;
 
using ApplicationServices.CommandHandlers;

namespace ApplicationServices.CrossCuttingConcerns
{
    public class ValidationCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private ICommandHandler<T> _decorated;
        private IValidator _validator;

        public ValidationCommandHandlerDecorator(IValidator<T> validator, ICommandHandler<T> cmdHandler)
        {
            _decorated = cmdHandler;
            _validator = validator;
        }
        
        public void Execute(T command)
        {
            var validationResult = _validator.Validate(command);
            if (!validationResult.IsValid)
            {
                var msg = BuildErrorMessage(validationResult.Errors);
                throw new ValidationException(msg, validationResult.Errors);
            }
            else { _decorated.Execute(command); }
        }

        private string BuildErrorMessage(IEnumerable<ValidationFailure> errors)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var error in errors) { sb.AppendLine(error.ErrorMessage); }
            return sb.ToString();
            
        }
    }
}
