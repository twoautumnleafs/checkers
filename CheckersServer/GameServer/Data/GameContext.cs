using Microsoft.EntityFrameworkCore;
using GameServer.Models;
namespace GameServer.Data
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        // DbSet для работы с таблицей "Scores"
        public DbSet<Score> Scores { get; set; }

        // DbSet для работы с таблицей "Ratings"
        public DbSet<Rating> Ratings { get; set; }

        // DbSet для работы с таблицей "Comments"
        public DbSet<Comment> Comments { get; set; }

        // Дополнительные DbSet'ы для других сущностей
    }
}