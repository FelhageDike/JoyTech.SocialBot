using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBotExtension.Definitions.Options;

namespace TelegramBotExtension.Definitions.Options
{
    /// <summary>
    /// Adds settings to IOptions
    /// </summary>
    public class OptionsDefinition : Definition
    {
        public override void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("telegramsettings.json");
            IConfiguration telegramConfiguration = configurationBuilder.Build();

            services.Configure<TelegramSettings>(telegramConfiguration);
        }
    }
}
