using Gamestudio.Models;
using System.Collections.Generic;

namespace Gamestudio.Services
{
    public interface ICommentService
    {
        void AddComment(Comment comment);  // Метод для добавления комментария
        List<Comment> GetCommentsForGame(int gameId);  // Метод для получения комментариев для конкретной игры
    }
}