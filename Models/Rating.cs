using System;

namespace Gamestudio.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int GameId { get; set; }  // Идентификатор игры, к которой относится рейтинг
        public string PlayerName { get; set; }
        public int RatingValue { get; set; }  // Оценка от 1 до 5
        public DateTime Date { get; set; }  // Дата добавления рейтинга
    }
}