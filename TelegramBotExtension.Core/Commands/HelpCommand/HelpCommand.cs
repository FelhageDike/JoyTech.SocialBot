using Fclp;
using Fclp.Internals.Extensions;
using Microsoft.Extensions.Logging;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotExtension.Core.Commands.Base;

namespace TelegramBotExtension.Core.Commands.HelpCommand
{
    [Command("/help")]
    public class HelpCommand : ICommandHandler
    {
        private readonly ILogger<HelpCommand> _logger;

        public HelpCommand(ILogger<HelpCommand> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, UserCommand command)
        {
            if (command.Arguments is null || command.Arguments.Length == 0)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, 
                    "This is the help command.\n\r You can run this command with the arguments --chatid or --userid");
            }

            var arguments = await ParseArguments(botClient, update, command);

            if (arguments.IsChatId)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id,
                    $"Chat id {update.Message.Chat.Id}");
            }
            if (arguments.IsUserId)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id,
                    $"Your user id {update.Message.From.Id}");
            }

            StringBuilder stringBuilder = new StringBuilder((int)command.Arguments?.Length);
            command.Arguments.ForEach(x => stringBuilder.Append(x + " "));
            _logger.LogInformation($"Command {command.Command} is executed with arguments {stringBuilder.ToString()}");
        }

        private async Task<HelpArguments> ParseArguments(ITelegramBotClient botClient, Update update, UserCommand command)
        {
            var parser = new FluentCommandLineParser<HelpArguments>();
            parser.Setup(arg => arg.IsChatId).As('c', "chatid").SetDefault(false);
            parser.Setup(arg => arg.IsUserId).As('u', "userid").SetDefault(false);

            var cmdParserResult = parser.Parse(command.Arguments?.ToArray());
            if (cmdParserResult.HasErrors)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, cmdParserResult.ErrorText);
                throw new Exception(cmdParserResult.ErrorText);
            }

            return parser.Object;
        }
    }
}
