using TOF.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TOF.Core.Configuration
{
    public class Configuration : IConfiguration
    {
        private IList<IConfigurationProvider> _providers = null;

        public Configuration(IList<IConfigurationProvider> Providers)
        {
            ParameterChecker.NotNull(Providers);

            if (!Providers.Any())
                throw new InvalidOperationException("ERROR_NO_PROVIDER_FOUND");

            this._providers = Providers;
        }

        public string this[string Key]
        {
            get
            {
                string value = null;

                foreach (var provider in this._providers)
                {
                    if (provider.TryGet(Key, out value))
                        return value;
                }

                return null;
            }
        }
    }
}
