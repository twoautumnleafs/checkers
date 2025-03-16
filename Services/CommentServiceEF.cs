using Gamestudio.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Gamestudio.Services
{
    public class CommentServiceEF : ICommentService
    {
        private readonly CheckersGameContext _context;

        // Конструктор принимает контекст базы данных через DI
        public CommentServiceEF(CheckersGameContext context)
        {
            _context = context;
        }

        // Реализация метода для добавления комментария
        public void AddComment(Comment comment)
        {
            comment.EnsureUtc();
            _context.Comments.Add(comment);  // Добавляем комментарий в коллекцию
            _context.SaveChanges();          // Сохраняем изменения в базе данных
        }

        // Реализация метода для получения комментариев для конкретной игры
        public List<Comment> GetCommentsForGame(int gameId)
        {
            return _context.Comments
                .Where(c => c.GameId == gameId)  // Фильтруем по id игры
                .ToList();                      // Преобразуем результат в список
        }
    }
}