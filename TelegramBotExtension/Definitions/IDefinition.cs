using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TelegramBotExtension.Definitions
{
    public interface IDefinition
    {
        void ConfigureService(IServiceCollection services, IConfiguration configuration);
        bool Enabled { get; }
    }
}
