using tofx.Core.Configuration.DotNet;

namespace tofx.Core.Configuration
{
    /// <summary>
    /// KF-NG Framework 組態系統 .NET Framework 組態提供者擴充程式。
    /// </summary>
    public static class DotNetConfigurationBuilderExtensions
    {
        /// <summary>
        /// 加入 .NET Framework 組態提供者到 KF-NG 組態系統。
        /// </summary>
        /// <param name="builder">KF-NG 組態建置器。</param>
        /// <returns>已經加入 .NET Framework 組態提供者擴充的 KF-NG 組態建置器。</returns>
        public static IConfigurationBuilder AddDotNetConfiguration(this IConfigurationBuilder builder)
        {
            builder.Add(new DotNetConfigurationProvider());
            return builder;
        }
    }
}
