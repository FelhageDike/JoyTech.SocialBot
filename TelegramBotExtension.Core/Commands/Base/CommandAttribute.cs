namespace TelegramBotExtension.Core.Commands.Base
{
    /// <summary>
    /// The attribute in which the text representation of the command is written
    /// </summary>
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// Command name
        /// </summary>
        public string Command { get; set; }

        public CommandAttribute(string command) 
        {
            this.Command = command;
        }
    }
}
