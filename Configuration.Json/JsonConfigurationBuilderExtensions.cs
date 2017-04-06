using tofx.Core.Configuration.Json;

namespace tofx.Core.Configuration
{
    public static class JsonConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddJsonFile(this IConfigurationBuilder builder, string path)
        {
            builder.Add(new JsonConfigurationProvider(path));
            return builder;
        }

        public static IConfigurationBuilder AddJsonFile(this IConfigurationBuilder builder, string path, bool optional)
        {
            builder.Add(new JsonConfigurationProvider(path, optional));
            return builder;
        }
    }
}
