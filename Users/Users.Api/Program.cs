using Common.Interfaces;
using Common.Models;
using Common.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using Users.Core.Interfaces;
using Users.Core.Services;
using Users.Data.DatabaseContext;
using Users.Data.Repositories;

namespace Users.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ServicesConfig(builder.Services);
            AuthConfig(builder.Services);
            DocsConfig(builder.Services);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }

        private static void DocsConfig(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private static void AuthConfig(IServiceCollection services)
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
                            Encoding.UTF8.GetBytes(AppConfig.GetSetting("JwtOptions:SecretKey"!)))
                    };
                });

            services.AddAuthorization();
        }

        private static void ServicesConfig(IServiceCollection services)
        {
            services.Configure<JwtOptions>(AppConfig.GetSection(nameof(JwtOptions)));

            DIConfig(services);

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            });

            DbContextConfig(services);
        }

        private static void DIConfig(IServiceCollection services)
        {
            services.AddSingleton<IDbContextConfigurer<UsersDbContext>, UsersDbContextConfigurer>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtProvider, JwtProvider>();
        }

        private static void DbContextConfig(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var connectionString = serviceProvider
                .GetService<IDbContextConfigurer<UsersDbContext>>()!
                .GetConnectionString();

            services.AddDbContext<UsersDbContext>(options =>
                serviceProvider
                    .GetService<IDbContextConfigurer<UsersDbContext>>()!
                    .Configure(options, connectionString));
        }
    }
}