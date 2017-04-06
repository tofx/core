using TOF.Core.Abstractions;
using System;

namespace TOF.Core.DependencyInjection
{
    public class ObjectActivatingFailedException : Exception, ILoggableException
    {
        private string _failType = null;

        public ObjectActivatingFailedException(string FailType)
        {
            _failType = FailType;
        }

        public string WriteLog(ILogger logger)
        {
            throw new NotImplementedException();
        }
    }
}
