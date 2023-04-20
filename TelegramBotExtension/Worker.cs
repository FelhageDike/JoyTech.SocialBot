using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TelegramBotExtension.Definitions.Options;

namespace TelegramBotExtension;

internal class Worker : BackgroundService
{
    private readonly IUpdateHandler _updateHandler;
    private readonly IOptions<TelegramSettings> _options;
    private readonly TelegramBotClient _botClient;

    public Worker(IUpdateHandler updateHandler, IOptions<TelegramSettings> options)
    {
        _updateHandler = updateHandler;
        _options = options;
        _botClient = new TelegramBotClient(_options.Value.Token);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _botClient.StartReceiving(_updateHandler);
        return Task.CompletedTask;
    }
}
