using System;

namespace Gamestudio.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int GameId { get; set; }  // Идентификатор игры, к которой относится комментарий
        public string PlayerName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }  // Дата добавления комментария
    }
}