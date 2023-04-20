using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramBotExtension.Core.Commands.Base
{
    /// <summary>
    /// Interface for all command handlers
    /// </summary>
    public interface ICommandHandler 
    {
        Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, UserCommand command);
    }
}
