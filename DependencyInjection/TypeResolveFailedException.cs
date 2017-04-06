using TOF.Core.Abstractions;
using System;

namespace TOF.Core.DependencyInjection
{
    public class TypeResolveFailedException : Exception, ILoggableException
    {
        public string WriteLog(ILogger logger)
        {
            throw new NotImplementedException();
        }
    }
}