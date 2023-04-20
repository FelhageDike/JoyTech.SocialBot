using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace TelegramBotExtension
{
    public static class SerilogExtension
    {
        public static IHostBuilder ConfigureSerilog(this IHostBuilder builder) => builder
            .ConfigureLogging((loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
            })
            .UseSerilog((context, services, configuration) =>
            {
                var configurationBuilder = new ConfigurationBuilder();
                configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
                configurationBuilder.AddJsonFile("appsettings.json");
                IConfiguration Configuration = configurationBuilder.Build();

                configuration.ReadFrom.Configuration(Configuration);
            });
    }
}
