using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TelegramBot.DAL;

namespace TelegramBotExtension.Definitions.DbContext;

public class DbContextDefinition : Definition
{
    public override void ConfigureService(IServiceCollection services, IConfiguration configuration)
    {
       
        string connectionString = configuration.GetConnectionString("DefaultConnectionString");
        services.AddDbContext<DefaultDbContext>(options =>
            options.UseNpgsql(connectionString));
        
    }
}