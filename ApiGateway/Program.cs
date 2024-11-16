using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        CombineOcelotConfigurations(builder.Configuration, builder.Environment);
        builder.Services.AddOcelot(builder.Configuration);

        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseAuthorization();

        app.MapControllers();

        await app.UseOcelot();

        app.Run();
    }

    private static void CombineOcelotConfigurations(IConfigurationBuilder configurationBuilder, IWebHostEnvironment env)
    {
        configurationBuilder.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
        //var basePath = Directory.GetCurrentDirectory();
        //Console.WriteLine($"Starting loading {env.EnvironmentName} files");
        //var configFiles = new List<string> {
        //    $"ocelot.{env.EnvironmentName}.json",
        //    $"ocelot.{env.EnvironmentName}.tracks.json",
        //    $"ocelot.{env.EnvironmentName}.users.json"
        //};

        //foreach (var configFile in configFiles)
        //{
        //    var filePath = Path.Combine(basePath, configFile);
        //    if (File.Exists(filePath))
        //    {
        //        Console.WriteLine("Adding " + filePath + " file..");
        //        configurationBuilder.AddJsonFile(filePath, optional: false, reloadOnChange: true);
        //    }
        //    else
        //    {
        //        Console.WriteLine(filePath + " file do not exist");
        //    }
        //}

        configurationBuilder.AddOcelot(env);
    }
}
