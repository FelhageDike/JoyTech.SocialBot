using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot.Polling;
using TelegramBotExtension.Core.Commands.Base;
using TelegramBotExtension.Core.Handlers.Base;

namespace TelegramBotExtension.Definitions.TelegramBot
{
    public class TelegramBotDefinition : Definition
    {
        public override void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            var applicationTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.GetTypes());

            services.AddSingleton<CommandHandlerParser>();
            services.AddSingleton<UpdateHandlerManager>();

            var telegramBotUpdateHandlerTypes = applicationTypes.Where(t => t.GetInterfaces().Any(i => i == typeof(IApplicationUpdateHandler)));
            foreach (var telegramBotUpdateHandlerType in telegramBotUpdateHandlerTypes)
            {
                services.AddSingleton(telegramBotUpdateHandlerType);
            }

            var commandHandlerTypes = applicationTypes.Where(t => t.GetInterfaces().Any(i => i == typeof(ICommandHandler)));
            foreach (var commandHandlerType in commandHandlerTypes)
            {
                services.AddSingleton(commandHandlerType);
            }

            services.AddSingleton<IUpdateHandler, UpdateHandler>();
        }
    }
}
