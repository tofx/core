using System.Collections.Generic;

namespace tofx.Core.Configuration
{
    public abstract class ConfigurationProvider : IConfigurationProvider
    {
        protected IDictionary<string, string> ConfigurationValues { get; set; }

        public ConfigurationProvider()
        {
            ConfigurationValues = new Dictionary<string, string>();
        }

        public virtual void Load()
        {
        }

        public virtual void Set(string key, string value)
        {
            ConfigurationValues[key] = value;
        }

        public virtual bool TryGet(string key, out string value)
        {
            return ConfigurationValues.TryGetValue(key, out value);
        }
    }
}
