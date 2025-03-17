using GameServer.Models;
using GameServer.Data;
using System.Collections.Generic;
using System.Linq;
namespace GameServer.Services
{
    public class ScoreService
    {
        private readonly GameContext _context;

        public ScoreService(GameContext context)
        {
            _context = context;
        }

        public IEnumerable<Score> GetTopScores(string game)
        {
            return _context.Scores
                .Where(s => s.Game == game)
                .OrderByDescending(s => s.Points)
                .Take(10)
                .ToList();
        }

        public void AddScore(Score score)
        {
            _context.Scores.Add(score);
            _context.SaveChanges();
        }
    }
}