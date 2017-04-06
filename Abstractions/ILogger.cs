using System;

namespace TOF.Core.Abstractions
{
    public interface ILogger
    {
        void Write(LogTypes LogType, string Info, string Data = null);
        void Write(LogTypes LogType, Type LogRequestType, string Info, string Data = null);
        void Write<T>(LogTypes LogType, string Info, string Data = null);
    }
}
