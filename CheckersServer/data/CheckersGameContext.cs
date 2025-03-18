using Microsoft.EntityFrameworkCore;
using Gamestudio.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Gamestudio
{
    public class CheckersGameContext : DbContext
    {
        public DbSet<Score> Scores { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public CheckersGameContext(DbContextOptions<CheckersGameContext> options) : base(options) { }

        // **Конструктор без параметров для EF Migrations**
        public CheckersGameContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Читаем строку подключения из appsettings.json
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
}