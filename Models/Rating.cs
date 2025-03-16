using System;

namespace Gamestudio.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string PlayerName { get; set; }
        public int RatingValue { get; set; }
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