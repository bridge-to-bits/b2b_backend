using Microsoft.Extensions.Configuration;

namespace Common.Utils
{
    public static class AppConfig
    {
        private static readonly Lazy<IConfigurationRoot> _configuration = new(() =>
        {
            var baseDirectory = AppContext.BaseDirectory;
            var solutionDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.Parent.Parent.FullName;

            return new ConfigurationBuilder()
                .SetBasePath(solutionDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        });

        public static IConfigurationRoot Configuration => _configuration.Value;

        public static string GetConnectionString(string name) => Configuration.GetConnectionString(name);

        public static string GetSetting(string key) => Configuration[key];

        public static IConfigurationSection GetSection(string sectionName) => Configuration.GetSection(sectionName);
    }
}
