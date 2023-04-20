using System.Data;
using System;
using System.Reflection;

namespace TelegramBotExtension.Core.Commands.Base
{
    /// <summary>
    /// Helps to parse commands implementing <see cref="ICommandHandler"/>
    /// </summary>
    public class CommandHandlerParser
    {
        private readonly Dictionary<string, ICommandHandler> _commandsCache = new();

        public CommandHandlerParser(IServiceProvider serviceProvider) 
        {
            var commandTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t => t.GetInterfaces().Any(t => t == typeof(ICommandHandler)));

            if (commandTypes is null)
            {
                throw new ArgumentNullException(nameof(commandTypes));
            }

            foreach (var type in commandTypes)
            {
                var textCommandAttribute = type.GetCustomAttribute<CommandAttribute>();
                if (textCommandAttribute == null)
                    continue;

                if (_commandsCache.ContainsKey(textCommandAttribute.Command))
                    throw new InvalidConstraintException($"Repeating the command name: {textCommandAttribute.Command}");

                _commandsCache.Add(textCommandAttribute.Command, (ICommandHandler)serviceProvider.GetService(type)!);
            }
        }

        public bool TryParse(string commandText, out ICommandHandler commandHandler)
        {
            var commandName = commandText.Split(" ")[0];
            return _commandsCache.TryGetValue(commandName, out commandHandler!);
        }
    }
}
