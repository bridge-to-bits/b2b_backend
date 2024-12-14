using Core.Interfaces.Repositories;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Core.Interfaces.Services
{
    public static class AppConfig
    {
        private static readonly Lazy<IConfigurationRoot> _configuration = new(() =>
        {
            Env.Load();

            return new ConfigurationBuilder()
                .AddEnvironmentVariables()
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
