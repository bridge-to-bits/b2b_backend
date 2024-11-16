using Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Common.Utils
{
    public static class AppConfig
    {
        private static readonly Lazy<IConfigurationRoot> _configuration = new(() =>
        {
            var baseDirectory = AppContext.BaseDirectory;
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            return new ConfigurationBuilder()
                .SetBasePath(baseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();
        });

        public static IConfigurationRoot Configuration => _configuration.Value;

        public static string GetConnectionString(string name) => Configuration.GetConnectionString(name);

        public static string GetSetting(string key) => Configuration[key];

        public static IConfigurationSection GetSection(string sectionName) => Configuration.GetSection(sectionName);

        public static void DocsConfig(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void GeneralAuthConfig(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(GetSetting("JwtOptions:SecretKey"!)))
                    };
                });

            services.AddAuthorization();
        }

        public static void DbContextConfig<T>(IServiceCollection services) where T : DbContext
        {
            var serviceProvider = services.BuildServiceProvider();
            var connectionString = serviceProvider
                .GetService<IDbContextConfigurer<T>>()!
                .GetConnectionString();

            services.AddDbContext<T>(options =>
                serviceProvider
                    .GetService<IDbContextConfigurer<T>>()!
                    .Configure(options, connectionString));
        }
    }
}
