using System;

namespace Gamestudio.Models
{
    public class Score
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int ScoreValue { get; set; }
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