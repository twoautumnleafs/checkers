using GameStudioClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameStudioClient.Services
{
    public interface IRatingService
    {
        Task<List<Rating>> GetRatingsAsync(string gameName);
        Task AddRatingAsync(Rating rating);
    }
}