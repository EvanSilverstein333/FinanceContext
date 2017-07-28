using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RequestProcessors
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICommandService" in both code and config file together.
    [ServiceContract]
    public interface ICommandService
    {
        [OperationContract]
        void DoWork();
    }
}
