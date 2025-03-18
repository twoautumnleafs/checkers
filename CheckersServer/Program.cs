using Microsoft.EntityFrameworkCore;
using Gamestudio.Services;
using Gamestudio.UI;
using Gamestudio.Models;
using Microsoft.Extensions.DependencyInjection;  // Для DI
using Gamestudio;
namespace CheckersGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Строка подключения к базе данных PostgreSQL
            string connectionString = "Server=localhost;User id=postgres;Password=xbox;Database=gamestudio";  // Убедитесь, что это ваша реальная строка подключения

            // Создание контейнера для зависимостей
            var serviceProvider = new ServiceCollection()
                .AddDbContext<CheckersGameContext>(options => options.UseNpgsql(connectionString))  // Используем EF и строку подключения
                .AddScoped<IScoreService, ScoreServiceEF>()  // Сервис для работы с баллами через EF
                .AddScoped<ICommentService, CommentServiceEF>()  // Сервис для работы с комментариями через EF
                .AddScoped<IRatingService, RatingServiceEF>()  // Сервис для работы с рейтингами через EF
                .AddScoped<ConsoleUI>()  // Добавляем UI
                .BuildServiceProvider();

            // Получаем экземпляр ConsoleUI через DI
            var ui = serviceProvider.GetService<ConsoleUI>();

            // Запуск игры через консольный интерфейс
            ui.Start();
        }
    }
}