
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            builder.Services.AddOcelot(builder.Configuration);

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseAuthorization();

            app.MapControllers();

            await app.UseOcelot();

            app.Run();
        }
    }
}
