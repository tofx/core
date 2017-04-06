using System;
using System.Collections.Generic;

namespace TOF.Core.Configuration
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

        public virtual void Set(string Key, string Value)
        {
            ConfigurationValues[Key] = Value;
        }

        public virtual bool TryGet(string Key, out string Value)
        {
            return ConfigurationValues.TryGetValue(Key, out Value);
        }
    }
}
