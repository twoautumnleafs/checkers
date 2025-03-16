using System;

namespace Gamestudio.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string PlayerName { get; set; }
        public string Text { get; set; }  // ✅ Должно быть
        public DateTime Date { get; set; }
        public void EnsureUtc()
        {
            if (Date.Kind != DateTimeKind.Utc)
            {
                Date = Date.ToUniversalTime();
            }
        }
    }
}