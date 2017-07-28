
using System.Collections.Generic;
using FluentValidation;
using System.ServiceModel;
using ApplicationServices.CommandHandlers;
using FinanceManager.Contract.Commands;
using ValueObjects.Wcf;

namespace ApplicationServices.CrossCuttingConcerns
{


    public class ToWcfFaultTranslatorCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> decoratee;

        public ToWcfFaultTranslatorCommandHandlerDecorator(ICommandHandler<TCommand> decoratee)
        {
            this.decoratee = decoratee;
        }
        
        public void Execute(TCommand command)
        {
            try
            {
                this.decoratee.Execute(command);
            }
            catch (ValidationException ex)
            {
                // This ensures that validation errors are communicated to the client,
                // while other exceptions are filtered by WCF (if configured correctly).
                var errors = new List<MyValidationFailure>();
                foreach(var error in ex.Errors) { errors.Add(new MyValidationFailure(error.PropertyName, error.ErrorMessage)); }
                var validator = new MyValidator(errors,ex.Message);
                throw new FaultException<MyValidator>(validator,validator.ErrorMessage);
            }
        }
    }
}