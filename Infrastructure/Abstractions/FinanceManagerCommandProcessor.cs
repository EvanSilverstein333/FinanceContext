using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using ApplicationServices.CommandHandlers;
using FinanceManager.Contract.Commands;
using System.ServiceModel;
//using FinanceManager.Contract.Services;
using FluentValidation;
using ValueObjects.Wcf;
using System.Reflection;

namespace Infrastructure.Abstractions
{
    [ServiceContract()]
    [ServiceKnownType(nameof(GetKnownTypes))]
    public class FinanceManagerCommandProcessor //: ICommandService
    {
        //public static Type[] CommandTypes { get; set; }
        public FinanceManagerCommandProcessor()
        {
        }

        [OperationContract]
        [FaultContract(typeof(MyValidator))]
        [FaultContract(typeof(MyConcurrencyIndicator))]
        public void Submit(dynamic command)
        {
            try
            {
                Type commandHandlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());

                dynamic commandHandler = Bootstrapper.Container.GetInstance(commandHandlerType);
                commandHandler.Execute(command);

            }
            catch (Exception ex)
            {
                if (ex is FaultException == false)
                {
                    Bootstrapper.Logger.Error("Unknown error occured", ex); // unknown error
                    var faultException = WcfExceptionTranslatorDebugger.CreateFaultExceptionOrNull(ex);
                    if (faultException != null) { throw faultException; }
                }

                throw;
            }

        }




        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
        {
            var types = Bootstrapper.AllContractTypes.ToList();
            return types;

        }
    }
}
