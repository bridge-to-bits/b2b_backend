using Common.Interfaces;
using Common.Utils;
using Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Data.DatabaseContext;

public class B2BDbContextConfigurer : IDbContextConfigurer<B2BDbContext>
{
    public void Configure(DbContextOptionsBuilder<B2BDbContext> builder, string connectionString)
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
