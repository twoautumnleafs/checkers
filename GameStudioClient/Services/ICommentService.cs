using GameStudioClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStudioClient.Services
{
    public interface ICommentService
    {
        Task<List<Comment>> GetCommentsAsync(string gameName);
        Task AddCommentAsync(Comment comment);
    }
}