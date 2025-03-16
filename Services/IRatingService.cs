using Gamestudio.Models;
using System.Collections.Generic;

namespace Gamestudio.Services
{
    public interface IRatingService
    {
        void AddRating(Rating rating);  // Метод для добавления рейтинга
        List<Rating> GetRatingsForGame(int gameId);  // Метод для получения рейтингов для конкретной игры
    }
}