using Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Users.Data.DatabaseContext
{
    public class UsersDbContextConfigurer : IDbContextConfigurer<UsersDbContext>
    {
        public void Configure(DbContextOptionsBuilder<UsersDbContext> builder, string connectionString)
        {
            builder.UseMySQL(connectionString);
        }
        public void Configure(DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseMySQL(connectionString);
        }

        public string GetConnectionString()
        {
            var baseDirectory = AppContext.BaseDirectory;
            var solutionDirectory = Directory.GetParent(baseDirectory).Parent.Parent.Parent.Parent.Parent.FullName;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"{solutionDirectory}/appsettings.json")
                .Build();
            return configuration.GetConnectionString("UsersDbConnectionString") ?? "";
        }
    }
}
