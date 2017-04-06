namespace TOF.Core.Configuration
{
    public interface IConfigurationProvider
    {
        bool TryGet(string Key, out string Value);
        void Set(string Key, string Value);
        void Load();
    }
}
