using GameServer.Models;
using GameServer.Data;
using System.Collections.Generic;
using System.Linq;
namespace GameServer.Services
{
    public class CommentService
    {
        private readonly GameContext _context;

        public CommentService(GameContext context)
        {
            _context = context;
        }

        public IEnumerable<Comment> GetComments(string game)
        {
            return _context.Comments
                .Where(c => c.Game == game)
                .OrderByDescending(c => c.CommentedAt)
                .ToList();
        }

        public void AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }
    }
}