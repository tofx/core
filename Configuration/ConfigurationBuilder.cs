using System.Collections.Generic;

namespace TOF.Core.Configuration
{
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private IList<IConfigurationProvider> _providers = null;

        public ConfigurationBuilder()
        {
            this._providers = new List<IConfigurationProvider>();
        }

        public void Add(IConfigurationProvider Provider)
        {
            this._providers.Add(Provider);
        }

        public IConfiguration Build()
        {
            foreach (var provider in this._providers)
                provider.Load();

            return new Configuration(this._providers);
        }
    }
}
