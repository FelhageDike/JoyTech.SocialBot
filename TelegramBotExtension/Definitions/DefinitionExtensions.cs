using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TelegramBotExtension.Definitions
{
    public static class DefinitionExtensions
    {
        public static void AddDefinitions(this IServiceCollection services, IConfiguration configuration, params Type[] entryPointsAssembly)
        {
            var definitions = new List<IDefinition>();

            foreach (var entryPoint in entryPointsAssembly)
            {
                var types = entryPoint.Assembly.ExportedTypes.Where(t => !t.IsAbstract && typeof(IDefinition).IsAssignableFrom(t));
                var list = types.Select(Activator.CreateInstance).Cast<IDefinition>();
                var instances = list.Where(x => x.Enabled == true);
                definitions.AddRange(instances);
            }

            definitions.ForEach(definition => definition.ConfigureService(services, configuration));
        }
    }
}
