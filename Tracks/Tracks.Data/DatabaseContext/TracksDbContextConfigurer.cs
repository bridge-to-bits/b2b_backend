using Common.Interfaces;
using Common.Utils;
using Microsoft.EntityFrameworkCore;

namespace Tracks.Data.DatabaseContext;

public class TracksDbContextConfigurer : IDbContextConfigurer<TracksDbContext>
{
    public void Configure(DbContextOptionsBuilder<TracksDbContext> builder, string connectionString)
    {
        builder.UseMySQL(connectionString);
    }
    public void Configure(DbContextOptionsBuilder builder, string connectionString)
    {
        builder.UseMySQL(connectionString);
    }

    public string GetConnectionString()
    {
        return AppConfig.GetConnectionString("TracksDbConnectionString") ?? "";
    }
}
