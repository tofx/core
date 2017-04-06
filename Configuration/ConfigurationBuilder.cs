using System.Collections.Generic;

namespace tofx.Core.Configuration
{
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private IList<IConfigurationProvider> _providers = null;

        public ConfigurationBuilder()
        {
            _providers = new List<IConfigurationProvider>();
        }

        public void Add(IConfigurationProvider provider)
        {
            _providers.Add(provider);
        }

        public IConfiguration Build()
        {
            foreach (var provider in _providers)
                provider.Load();

            return new Configuration(_providers);
        }
    }
}
