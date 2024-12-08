
using Common.Interfaces;
using Common.Models;
using Common.Utils;
using System.Text.Json.Serialization;
using System.Text.Json;
using Data.DatabaseContext;
using Core.Interfaces;
using Data.Repositories;
using Core.Services;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ServicesConfig(builder.Services);
        AppConfig.GeneralAuthConfig(builder.Services);
        AppConfig.DocsConfig(builder.Services);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins("http://localhost:3000")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors("AllowFrontend");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }

    private static void ServicesConfig(IServiceCollection services)
    {
        services.Configure<GoogleDriveOptions>(AppConfig.GetSection(nameof(GoogleDriveOptions)));
        services.Configure<JwtOptions>(AppConfig.GetSection(nameof(JwtOptions)));

        DIConfig(services);

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
        });

        AppConfig.DbContextConfig<B2BDbContext>(services);
    }

    private static void DIConfig(IServiceCollection services)
    {
        services.AddSingleton<IDbContextConfigurer<B2BDbContext>, B2BDbContextConfigurer>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISocialRepository, SocialsRepository>();
        services.AddScoped<IGenreRepository, GenresRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtProvider, JwtProvider>(); 
        
        services.AddScoped<ITrackRepository, TrackRepository>();
        services.AddScoped<IGenreRepository, GenresRepository>();
        services.AddScoped<ITrackService, TrackService>();
        services.AddScoped<IGenreService, GenresService>();
    }
}
