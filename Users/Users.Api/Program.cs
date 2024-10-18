using Common.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
            builder.Services.AddControllers();
            builder.Services.AddSingleton<IDbContextConfigurer<UsersDbContext>, UsersDbContextConfigurer>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();

            var serviceProvider = builder.Services.BuildServiceProvider();
            var connectionString = serviceProvider
                .GetService<IDbContextConfigurer<UsersDbContext>>()!
                .GetConnectionString();

            builder.Services.AddDbContext<UsersDbContext>(options =>
                serviceProvider
                    .GetService<IDbContextConfigurer<UsersDbContext>>()!
                    .Configure(options, connectionString));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            AuthConfig(builder.Services, builder.Configuration);
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

        private static void AuthConfig(IServiceCollection services, IConfiguration config)
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
                            Encoding.UTF8.GetBytes(config["JwtOptions:SecretKey"]!))
                    };
                });

            //services.AddAuthorization();
        }

    }
}