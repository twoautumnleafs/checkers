using GameServer.Models;
using GameServer.Data;
using System.Collections.Generic;
using System.Linq;
namespace GameServer.Services
{
    public class RatingService
    {
        private readonly GameContext _context;

        public RatingService(GameContext context)
        {
            _context = context;
        }

        public IEnumerable<Rating> GetAllRatings()
        {
            return _context.Ratings.ToList();
        }

        public void AddRating(Rating rating)
        {
            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }
    }
}