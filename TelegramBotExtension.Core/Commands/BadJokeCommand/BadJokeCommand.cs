using System.Text;
using Fclp;
using Fclp.Internals.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.DAL;
using TelegramBotExtension.Core.Commands.AllUserCommand;
using TelegramBotExtension.Core.Commands.Base;

namespace TelegramBotExtension.Core.Commands.BadJokeCommand;
[Command("/badjoke")]
public class BadJokeCommand  : ICommandHandler
{
    private readonly DefaultDbContext _context;
    private readonly ILogger<BadJokeCommand> _logger;

        public BadJokeCommand(ILogger<BadJokeCommand> logger, DefaultDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken, UserCommand command)
        {
            if (command.Arguments is null || command.Arguments.Length == 0)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, 
                    "da sosi");
            }

            var message = update.Message;
            
            if (update.Message.Text.Contains("@"))
            {
                var user = message.Text[(message.Text.IndexOf("@" , StringComparison.Ordinal)+1)..];
                var telagramUserId = await _context.Users.FirstAsync(x => x.UserName == user);
                telagramUserId.Rating -= 100;
                await _context.SaveChangesAsync();
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, 
                    $"Я снял этому лоху,{telagramUserId.UserName}, баллы");
            }
            else
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, 
                    $"Введите тег пользователя через @");
            }
                
}
        
}