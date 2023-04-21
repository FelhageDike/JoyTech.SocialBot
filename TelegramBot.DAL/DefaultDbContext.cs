using Microsoft.EntityFrameworkCore;
using TelegramBot.DAL.Models;

namespace TelegramBot.DAL;

public class DefaultDbContext : DbContext
{
     public DbSet<User> Users { get; set; }

    public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
        : base(options)
    {
    }
}