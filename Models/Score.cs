using System;

namespace Gamestudio.Models
{
    public class Score
    {
        public int Id { get; set; }
        public string PlayerName { get; set; }
        public int ScoreValue { get; set; }
        public DateTime Date { get; set; }  // Дата записи счета
    }
}