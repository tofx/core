using TOF.Core.Configuration;
using TOF.Core.DependencyInjection;
using System;

namespace TOF.Core.Infrastructure
{
    public static class App
    {
        public static Container ServiceProviders { get; private set; }
        public static Environment Environment { get; set; }
        public static IConfiguration Configuration { get; set; }

        static App()
        {
            Environment = new Environment();
        }

        public static void Configure(
            Action<ContainerBuilder> serviceConfigurator,
            Action<ConfigurationBuilder> configurationConfigurator)
        {
            var containerBuilder = new ContainerBuilder();
            var configurationBuilder = new ConfigurationBuilder();

            serviceConfigurator?.Invoke(containerBuilder);
            configurationConfigurator?.Invoke(configurationBuilder);

            ServiceProviders = containerBuilder.Build();
            Configuration = configurationBuilder.Build();
        }
    }
}
