namespace TOF.Core.Configuration
{
    public interface IConfiguration
    {
        string this[string Key] { get; }
    }
}
