using System.Threading.Tasks;

namespace GameStudioClient.Services
{
    public interface IGameService
    {
        Task<string> MakeMoveAsync(string playerColor, string direction);
        Task<string> GetBoardAsync();
    }
}