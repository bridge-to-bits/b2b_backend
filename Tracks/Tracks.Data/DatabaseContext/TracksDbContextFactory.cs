using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Tracks.Data.DatabaseContext
{
    public class TracksDbContextFactory : IDesignTimeDbContextFactory<TracksDbContext>
    {
        public TracksDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TracksDbContext>();
            var configurer = new TracksDbContextConfigurer();
            var connectionString = configurer.GetConnectionString();
            configurer.Configure(builder, connectionString);

            return new TracksDbContext(builder.Options);
        }
    }
}
