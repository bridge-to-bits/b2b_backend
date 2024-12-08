using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Data.DatabaseContext;

public class B2BDbContextFactory : IDesignTimeDbContextFactory<B2BDbContext>
{
    public B2BDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<B2BDbContext>();
        var configurer = new B2BDbContextConfigurer();
        var connectionString = configurer.GetConnectionString();
        configurer.Configure(builder, connectionString);

        return new B2BDbContext(builder.Options);
    }
}
