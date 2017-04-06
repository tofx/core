namespace tofx.Core.Configuration
{
    public interface IConfigurationBuilder
    {
        void Add(IConfigurationProvider provider);
        IConfiguration Build();
    }
}
