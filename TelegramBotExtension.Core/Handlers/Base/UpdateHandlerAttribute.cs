using Telegram.Bot.Types.Enums;

namespace TelegramBotExtension.Core.Handlers.Base
{
    /// <summary>
    /// Attribute by which the factory will search for the handler
    /// </summary>
    public class UpdateHandlerAttribute : Attribute
    {
        public UpdateType UpdateType { get; }
        
        public UpdateHandlerAttribute(UpdateType updateType)
        {
            this.UpdateType = updateType;
        }
    }
}
