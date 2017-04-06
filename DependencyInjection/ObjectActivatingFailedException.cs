using tofx.Core.Abstractions;
using System;

namespace tofx.Core.DependencyInjection
{
    public class ObjectActivatingFailedException : Exception, ILoggableException
    {
        private string _failType = null;

        public ObjectActivatingFailedException(string failType)
        {
            _failType = failType;
        }

        public string WriteLog(ILogger logger)
        {
            throw new NotImplementedException();
        }
    }
}
