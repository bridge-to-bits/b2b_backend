using Common.Interfaces;
using Users.Core.Interfaces;
using Users.Core.Services;
using Users.Data.DatabaseContext;
using Users.Data.Repositories;

namespace Users.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSingleton<IDbContextConfigurer<UsersDbContext>, UsersDbContextConfigurer>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            var serviceProvider = builder.Services.BuildServiceProvider();
            var connectionString = serviceProvider
                .GetService<IDbContextConfigurer<UsersDbContext>>()!
                .GetConnectionString();

            builder.Services.AddDbContext<UsersDbContext>(options =>
                serviceProvider
                    .GetService<IDbContextConfigurer<UsersDbContext>>()!
                    .Configure(options, connectionString));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}