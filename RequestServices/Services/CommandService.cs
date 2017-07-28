using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ApplicationServices.CommandHandlers;
using FinanceManager.Contract.Commands;


namespace RequestServices.Services
{
    //[ServiceKnownType(nameof(GetKnownCommandTypes))]
    //public class CommandService : ICommandService
    //{
    //    private ICommandFactory _cmdFactory;
    //    public static Type[] CommandTypes { get; set; }

    //    public CommandService(ICommandFactory cmdFactory)
    //    {
    //        _cmdFactory = cmdFactory;
    //    }
    //    public void Execute(dynamic command)
    //    {
    //        _cmdFactory.Execute(command);
    //        //Type commandHandlerType = typeof(ICommandHandler<>)
    //        //.MakeGenericType(command.GetType());

            
    //        //dynamic commandHandler = Bootstrapper.GetInstance(commandHandlerType);

    //        //commandHandler.Handle(command);
    //    }

    //    static Type[] GetKnownCommandTypes()
    //    {
    //        return CommandTypes;
    //    }
    //}
}
