using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBotExtension;
using TelegramBotExtension.Definitions;

public class Program
{
    private static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .ConfigureSerilog()
            .Build()
            .Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureServices((context, service) =>
        {
            service.AddDefinitions(context.Configuration, typeof(Program));
            service.AddHostedService<Worker>();
        });
}