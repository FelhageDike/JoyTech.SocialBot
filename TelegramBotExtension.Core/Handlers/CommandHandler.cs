using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotExtension.Core.Commands.Base;
using TelegramBotExtension.Core.Handlers.Base;

namespace TelegramBotExtension.Core.Handlers
{
    /// <summary>
    /// Executes text commands
    /// </summary>
    public class CommandHandler : IApplicationUpdateHandler
    {
        private readonly ILogger<CommandHandler> _logger;
        private readonly CommandHandlerParser _commandHandlerParser;

        public CommandHandler(ILogger<CommandHandler> logger, CommandHandlerParser commandHandlerParser)
        {
            _logger = logger;
            _commandHandlerParser = commandHandlerParser;
        }

        public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var splitText = update.Message?.Text?.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var commandName = splitText[0];
            var commandArguments = splitText.Skip(1).ToArray();
            for (int i = 0; i < commandArguments.Length; i++)
            {
                commandArguments[i] = commandArguments[i].Replace("—", "--");
            }

            var userCommand = new UserCommand
            {
                Command = commandName,
                Arguments = commandArguments
            };

            if (_commandHandlerParser.TryParse(commandName, out var command))
            {
                await command.HandleAsync(botClient, update, cancellationToken, userCommand);
            }
        }
    }
}
