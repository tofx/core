using System;

namespace tofx.Core.Abstractions
{
    public interface ILogger
    {
        void Write(LogTypes logType, string info, string data = null);
        void Write(LogTypes logType, Type logRequestType, string info, string data = null);
        void Write<T>(LogTypes logType, string info, string data = null);
    }
}
