using GameStudioClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStudioClient.Services
{
    public interface IScoreService
    {
        Task<List<Score>> GetTopScoresAsync(string gameName);
        Task AddScoreAsync(Score score);
    }
}