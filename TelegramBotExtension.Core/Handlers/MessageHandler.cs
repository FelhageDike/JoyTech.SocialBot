using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotExtension.Core.Handlers.Base;

namespace TelegramBotExtension.Core.Handlers
{
    /// <summary>
    /// Processes messages with text
    /// </summary>
    [UpdateHandler(UpdateType.Message)]
    public class MessageHandler : IApplicationUpdateHandler
    {
        private readonly ILogger<MessageHandler> _logger;
        private readonly CommandHandler _commandHandler;

        public MessageHandler(ILogger<MessageHandler> logger, CommandHandler commandHandler)
        {
            _logger = logger;
            _commandHandler = commandHandler;
        }

        public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            if(message is null)
            {
                return;
            }

            if (message.Text is not null && message.Text.StartsWith("/"))
            {
                try
                {
                    await _commandHandler.HandleAsync(botClient, update, cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                return;
            }

            //your handler
        }
    }
}
