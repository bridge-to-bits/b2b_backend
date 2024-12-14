using System.Text.Json.Serialization;
using System.Text.Json;
using Data.DatabaseContext;
using Core.Interfaces.Repositories;
using Data.Repositories;
using Core.Services;
using Core.Interfaces.Services;
using Core.Interfaces.Auth;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddEnvironmentVariables();

        ServicesConfig(builder.Services);
        AppConfig.GeneralAuthConfig(builder.Services);
        AppConfig.DocsConfig(builder.Services);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins(AppConfig.GetSetting("FRONT_URL"))
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });

            options.AddPolicy("AllowLocal", policy =>
            {
                policy.WithOrigins(AppConfig.GetSetting("LOCAL_URL"))
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
        services.AddScoped<IPerformerRepository, PerformerRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITrackRepository, TrackRepository>();
        services.AddScoped<IGenreRepository, GenresRepository>();
        services.AddScoped<ITrackService, TrackService>();
        services.AddScoped<IGenreService, GenresService>();
    }
}
