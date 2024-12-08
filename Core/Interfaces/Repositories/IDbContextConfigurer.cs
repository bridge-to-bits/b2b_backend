using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces.Repositories
{
    public interface IDbContextConfigurer<T> where T : DbContext
    {
        void Configure(DbContextOptionsBuilder builder, string connectionString);
        void Configure(DbContextOptionsBuilder<T> builder, string connectionString);
        string GetConnectionString();
    }
}
