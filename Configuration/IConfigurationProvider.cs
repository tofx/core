namespace tofx.Core.Configuration
{
    public interface IConfigurationProvider
    {
        bool TryGet(string key, out string value);
        void Set(string key, string value);
        void Load();
    }
}
