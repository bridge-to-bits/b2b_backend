using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Users.Data.DatabaseContext
{
    public class UsersDbContextFactory : IDesignTimeDbContextFactory<UsersDbContext>
    {
        public UsersDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<UsersDbContext>();
            var configurer = new UsersDbContextConfigurer();
            var connectionString = configurer.GetConnectionString();
            configurer.Configure(builder, connectionString);

            return new UsersDbContext(builder.Options);
        }
    }
}
