using System;
using Gamestudio.Services;
using Gamestudio.UI;  // Путь к классу ConsoleUI
using Gamestudio.Models; // Путь к моделям (Score, Comment, Rating)

namespace CheckersGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Строка подключения к базе данных PostgreSQL
            string connectionString = "Host=localhost;Username=postgres;Password=xbox;Database=gamestudio";  // Убедитесь, что это ваша реальная строка подключения

            // Создание экземпляров сервисов
            var scoreService = new ScoreServiceJDBC(connectionString);  // Реализуйте сервисы в соответствии с JDBC-стилем
            var commentService = new CommentServiceJDBC(connectionString);
            var ratingService = new RatingServiceJDBC(connectionString);

            // Создание экземпляра UI
            var ui = new ConsoleUI(scoreService, commentService, ratingService);

            // Запуск игры через консольный интерфейс
            ui.Start();
        }
    }
}