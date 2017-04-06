using tofx.Core.Abstractions;
using System;

namespace tofx.Core.DependencyInjection
{
    public class TypeResolveFailedException : Exception, ILoggableException
    {
        public string WriteLog(ILogger logger)
        {
            throw new NotImplementedException();
        }
    }
}