
using System;
using System.Collections.Generic;
using System.ServiceModel;
using FluentValidation;
using FluentValidation.Results;
using ValueObjects.Wcf;

namespace Infrastructure.Abstractions
{

    public static class WcfExceptionTranslatorDebugger
    {
        public static FaultException CreateFaultExceptionOrNull(Exception exception)
        {

#if DEBUG
            return new FaultException(exception.ToString());
#else
            return null;
#endif
        }
    }
}