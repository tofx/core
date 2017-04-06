using TOF.Core.Configuration.Json;

namespace TOF.Core.Configuration
{
    public static class JsonConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddJsonFile(this IConfigurationBuilder builder, string Path)
        {
            builder.Add(new JsonConfigurationProvider(Path));
            return builder;
        }

        public static IConfigurationBuilder AddJsonFile(this IConfigurationBuilder builder, string Path, bool optional)
        {
            builder.Add(new JsonConfigurationProvider(Path, optional));
            return builder;
        }
    }
}
