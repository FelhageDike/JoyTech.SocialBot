using System.Text;
using System.Threading.Tasks.Dataflow;
using Fclp;
using Fclp.Internals.Extensions;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.DAL;
using TelegramBotExtension.Core.Commands.Base;
using User = TelegramBot.DAL.Models.User;
namespace TelegramBotExtension.Core.Commands.AddUserCommand;
[Command("/user")]
public class AddUserCommand :  ICommandHandler
{
    private readonly DefaultDbContext _context;
    private readonly ILogger<AddUserCommand> _logger;

        public AddUserCommand(ILogger<AddUserCommand> logger, DefaultDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, Base.UserCommand command)
        {
            var message = update.Message;
            if (command.Arguments is null || command.Arguments.Length == 0)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, 
                    "Это команда по добавлению пользователя.\n\r Вы можете запустить ее с аргументом --add");
            }

            var arguments = await ParseArguments(botClient, update, command);

            if (arguments.Add)
            {
                var userId = message.From.Id;
                var userName = message.From.Username;
                var user = new TelegramBot.DAL.Models.User
                {
                    Rating = 100,
                    TelegramId = userId.ToString(),
                    UserName = userName,
                    Id = Guid.NewGuid(),
                };
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"Я добавил {message.From.Username} в список.");
            }
            
            StringBuilder stringBuilder = new StringBuilder((int)command.Arguments?.Length);
            command.Arguments.ForEach(x => stringBuilder.Append(x + " "));
            _logger.LogInformation($"Command {command.Command} is executed with arguments {stringBuilder.ToString()}");
        }

        private async Task<AddUserArguments> ParseArguments(ITelegramBotClient botClient, Update update, Base.UserCommand command)
        {
            var parser = new FluentCommandLineParser<AddUserArguments>();
            parser.Setup(arg => arg.Add).As('a', "Add").SetDefault(false);

            var cmdParserResult = parser.Parse(command.Arguments?.ToArray());
            if (cmdParserResult.HasErrors)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, cmdParserResult.ErrorText);
                throw new Exception(cmdParserResult.ErrorText);
            }

            return parser.Object;
        }
}