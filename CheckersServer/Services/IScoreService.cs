using Gamestudio.Models;
using System.Collections.Generic;

namespace Gamestudio.Services
{
    public interface IScoreService
    {
        void AddScore(Score score);  // Метод для добавления нового счета
        List<Score> GetTopScores(int top);  // Метод для получения топ-10 счетов
        Score GetScoreById(int id);  // Метод для получения счета по ID
    }
}