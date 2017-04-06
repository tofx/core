namespace tofx.Core.Abstractions
{
    public interface ILoggableException
    {
        string WriteLog(ILogger logger);
    }
}
