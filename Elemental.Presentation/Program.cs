using Elemental.BusinessLogic;
using Elemental.BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = new HostBuilder()
             .ConfigureAppConfiguration((hostingContext, config) =>
             {
                 config.AddJsonFile("appsettings.json", optional: true);
             })
             .ConfigureServices((hostContext, services) =>
             {
                 services.Configure<PeriodicElementsOptions>(hostContext.Configuration.GetSection("SymbolSettings"));
                 services.AddTransient<IElementalWordsService, ElementalWordsService>();
             });

        var host = builder.Build();

        using (var serviceScope = host.Services.CreateScope())
        {
            var services = serviceScope.ServiceProvider;

            try
            {
                var myService = services.GetRequiredService<IElementalWordsService>();
                Console.WriteLine(JsonConvert.SerializeObject(myService.ElementalForms("snack")));
                Console.WriteLine(JsonConvert.SerializeObject(myService.ElementalForms("beach")));
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred.");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}