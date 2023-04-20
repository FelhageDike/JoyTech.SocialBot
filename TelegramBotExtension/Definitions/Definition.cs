using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TelegramBotExtension.Definitions
{
    public class Definition : IDefinition
    {
        public virtual void ConfigureService(IServiceCollection services, IConfiguration configuration) { }
        public virtual bool Enabled => true;
    }
}
