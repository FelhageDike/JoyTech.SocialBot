using Fclp;
using Fclp.Internals.Extensions;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.DAL;
using TelegramBotExtension.Core.Commands.Base;

namespace TelegramBotExtension.Core.Commands.AllUserCommand;
[Command("/getall")]
public class AllUserCommand : ICommandHandler
{
    private readonly DefaultDbContext _context;
    private readonly ILogger<AllUserCommand> _logger;

        public AllUserCommand(ILogger<AllUserCommand> logger, DefaultDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, UserCommand command)
        {
            if (command.Arguments is null || command.Arguments.Length == 0)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, 
                    "This is the help command.\n\r You can run this command with the arguments --Get");
            }

            var arguments = await ParseArguments(botClient, update, command);

            if (arguments.Get)
            {
                var sb = new StringBuilder();
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Жри всех юзеров");
                var users = await _context.Users.ToListAsync();
                foreach (var user in users)
                {
                    sb.Append($"{user.UserName} | {user.Rating}{Environment.NewLine}");
                }
                
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, sb.ToString());
            }
            
            StringBuilder stringBuilder = new StringBuilder((int)command.Arguments?.Length);
            command.Arguments.ForEach(x => stringBuilder.Append(x + " "));
            _logger.LogInformation($"Command {command.Command} is executed with arguments {stringBuilder.ToString()}");
        }

        private async Task<AllUserArguments> ParseArguments(ITelegramBotClient botClient, Update update, UserCommand command)
        {
            var parser = new FluentCommandLineParser<AllUserArguments>();
            parser.Setup(arg => arg.Get).As('g', "Get").SetDefault(false);

            var cmdParserResult = parser.Parse(command.Arguments?.ToArray());
            if (cmdParserResult.HasErrors)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, cmdParserResult.ErrorText);
                throw new Exception(cmdParserResult.ErrorText);
            }

            return parser.Object;
        }
}