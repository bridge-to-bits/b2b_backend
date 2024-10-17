using Microsoft.EntityFrameworkCore;

namespace Common.Interfaces
{
    public interface IDbContextConfigurer<T> where T : DbContext
    {
        void Configure(DbContextOptionsBuilder builder, string connectionString);
        void Configure(DbContextOptionsBuilder<T> builder, string connectionString);
        string GetConnectionString();
    }
}
