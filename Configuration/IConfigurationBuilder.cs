namespace TOF.Core.Configuration
{
    public interface IConfigurationBuilder
    {
        void Add(IConfigurationProvider Provider);
        IConfiguration Build();
    }
}
