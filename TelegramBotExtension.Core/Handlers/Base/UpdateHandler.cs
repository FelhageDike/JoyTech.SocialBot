using Microsoft.Extensions.Logging;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramBotExtension.Core.Handlers.Base
{
    /// <summary>
    /// Implementation of the <see cref="IUpdateHandler"/> interface in telegram.bot
    /// </summary>
    public class UpdateHandler : IUpdateHandler
    {
        private readonly ILogger<UpdateHandler> _logger;
        private readonly UpdateHandlerManager _updateHandlerManager;

        public UpdateHandler(ILogger<UpdateHandler> logger, UpdateHandlerManager updateHandlerManager)
        {
            _logger = logger;
            _updateHandlerManager = updateHandlerManager;
        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception.Message);
            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                var handler = _updateHandlerManager.GetHandler(update.Type);
                await handler.HandleAsync(botClient, update, cancellationToken);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
