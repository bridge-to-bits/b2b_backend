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
            options.AddPolicy("AllowAll", policy =>
            {
                var frontendUrl = AppConfig.GetSetting("FRONT_URL");
                var localUrl = AppConfig.GetSetting("LOCAL_URL");

                policy.WithOrigins(frontendUrl, localUrl)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        var app = builder.Build();

        //app.UseMiddleware<TokenMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.Run();
    }

    private static void ServicesConfig(IServiceCollection services)
    {
        services.Configure<GoogleDriveOptions>(AppConfig.GetSection(nameof(GoogleDriveOptions)));
        services.Configure<JwtOptions>(AppConfig.GetSection(nameof(JwtOptions)));
        services.Configure<MailerOptions>(AppConfig.GetSection(nameof(MailerOptions)));

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
        services.AddScoped<IProducerRepository, ProducerRepository>();
        services.AddScoped<INewsRespository, NewsRepository>();
        services.AddScoped<IRatingRepository, RatingsRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITrackRepository, TrackRepository>();
        services.AddScoped<IGenreRepository, GenresRepository>();
        services.AddScoped<ITrackService, TrackService>();
        services.AddScoped<IGenreService, GenresService>();
        services.AddScoped<IPerformerService, PerformerService>();
        services.AddScoped<IProducerService, ProducerService>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<INewsService, NewsService>();

    }
}
