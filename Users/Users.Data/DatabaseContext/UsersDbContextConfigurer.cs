using Common.Interfaces;
using Common.Utils;
using Microsoft.EntityFrameworkCore;

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
            return AppConfig.GetConnectionString("UsersDbConnectionString") ?? "";
        }
    }
}
