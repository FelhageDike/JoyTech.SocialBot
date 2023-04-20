using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramBotExtension.Core.Handlers.Base
{
    /// <summary>
    /// Interface for all handlers by type
    /// </summary>
    public interface IApplicationUpdateHandler 
    {
        /// <summary>
        /// Execution of the handler's work
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}
