using Common.Interfaces;
using Common.Models;
using Common.Utils;
using System.Text.Json.Serialization;
using System.Text.Json;
using Users.Core.Interfaces;
using Users.Core.Services;
using Users.Data.DatabaseContext;
using Users.Data.Repositories;

namespace Users.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ServicesConfig(builder.Services);
        AppConfig.GeneralAuthConfig(builder.Services);
        AppConfig.DocsConfig(builder.Services);

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

    private static void ServicesConfig(IServiceCollection services)
    {
        services.Configure<JwtOptions>(AppConfig.GetSection(nameof(JwtOptions)));

        DIConfig(services);

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });

        AppConfig.DbContextConfig<UsersDbContext>(services);
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
}