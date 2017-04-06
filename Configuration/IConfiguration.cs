namespace tofx.Core.Configuration
{
    public interface IConfiguration
    {
        string this[string key] { get; }
    }
}
