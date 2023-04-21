namespace TelegramBot.DAL.Models;

public class User
{
    public Guid Id { get; set; }
    public int Rating { get; set; }
    public string UserName { get; set; }
    public string TelegramId { get; set; }
}