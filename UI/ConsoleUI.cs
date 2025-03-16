using System;
using Gamestudio.Models;
using Gamestudio.Services;

namespace Gamestudio.UI
{
    public class ConsoleUI
    {
        private readonly IScoreService _scoreService;
        private readonly ICommentService _commentService;
        private readonly IRatingService _ratingService;

        public ConsoleUI(IScoreService scoreService, ICommentService commentService, IRatingService ratingService)
        {
            _scoreService = scoreService;
            _commentService = commentService;
            _ratingService = ratingService;
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Game Portal!");

            // Основное меню для взаимодействия с пользователем
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. Enter your score");
                Console.WriteLine("3. View top scores");
                Console.WriteLine("4. Leave a comment");
                Console.WriteLine("5. Rate the game");
                Console.WriteLine("6. Exit");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StartGame(); // Новый метод для старта игры
                        break;
                    case "2":
                        EnterScore();
                        break;
                    case "3":
                        ViewTopScores();
                        break;
                    case "4":
                        LeaveComment();
                        break;
                    case "5":
                        RateGame();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private void StartGame()
        {
            Console.WriteLine("Starting the game...");

            // Создаем экземпляр GameConsole и запускаем игру
            var gameConsole = new CheckersGame.GameConsole();
            gameConsole.Play(); // Здесь вызывается метод Play, который запускает игру
        }

        private void EnterScore()
        {
            Console.WriteLine("Enter your name:");
            var playerName = Console.ReadLine();
            Console.WriteLine("Enter your score:");
            var scoreValue = int.Parse(Console.ReadLine());

            var score = new Score
            {
                PlayerName = playerName,
                ScoreValue = scoreValue,
                Date = DateTime.Now
            };

            _scoreService.AddScore(score);
            Console.WriteLine("Score saved!");
        }

        private void ViewTopScores()
        {
            Console.WriteLine("Top 10 Scores:");
            var topScores = _scoreService.GetTopScores(10);
            foreach (var score in topScores)
            {
                Console.WriteLine($"{score.PlayerName}: {score.ScoreValue} (Date: {score.Date})");
            }
        }

        private void LeaveComment()
        {
            Console.WriteLine("Enter your name:");
            var playerName = Console.ReadLine();
            Console.WriteLine("Enter the game ID:");
            var gameId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter your comment:");
            var text = Console.ReadLine();

            var comment = new Comment
            {
                GameId = gameId,
                PlayerName = playerName,
                Text = text,
                Date = DateTime.Now
            };

            _commentService.AddComment(comment);
            Console.WriteLine("Comment added!");
        }

        private void RateGame()
        {
            Console.WriteLine("Enter your name:");
            var playerName = Console.ReadLine();
            Console.WriteLine("Enter the game ID:");
            var gameId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter your rating (1-5):");
            var ratingValue = int.Parse(Console.ReadLine());

            var rating = new Rating
            {
                GameId = gameId,
                PlayerName = playerName,
                RatingValue = ratingValue,
                Date = DateTime.Now
            };

            _ratingService.AddRating(rating);
            Console.WriteLine("Rating added!");
        }
    }
}
