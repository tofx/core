using tofx.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace tofx.Core.Configuration
{
    public class Configuration : IConfiguration
    {
        private readonly IList<IConfigurationProvider> _providers;

        public Configuration(IList<IConfigurationProvider> providers)
        {
            ParameterChecker.NotNull(providers);

            if (!providers.Any())
                throw new InvalidOperationException("ERROR_NO_PROVIDER_FOUND");

            _providers = providers;
        }

        public string this[string key]
        {
            get
            {
                foreach (var provider in _providers)
                {
                    if (provider.TryGet(key, out string value))
                        return value;
                }

                return null;
            }
        }
    }
}
