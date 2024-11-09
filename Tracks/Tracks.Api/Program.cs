using Common.Utils;
using Common.Models;
using Common.Interfaces;
using Tracks.Core.Interfaces;
using Tracks.Data.Repositories;
using Tracks.Core.Services;
using Tracks.Data.DatabaseContext;

namespace Tracks.Api;

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

        services.AddControllers();

        DIConfig(services);

        AppConfig.DbContextConfig<TracksDbContext>(services);
    }

    private static void DIConfig(IServiceCollection services)
    {
        services.AddSingleton<IDbContextConfigurer<TracksDbContext>, TracksDbContextConfigurer>();
        services.AddScoped<ITrackRepository, TrackRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<ITrackService, TrackService>();
        services.AddScoped<IGenreService, GenreService>();
    }
}
