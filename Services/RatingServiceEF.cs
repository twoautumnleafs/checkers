using Gamestudio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamestudio.Services
{
    public class RatingServiceEF : IRatingService
    {
        private readonly CheckersGameContext _context;

        public RatingServiceEF(CheckersGameContext context)
        {
            _context = context;
        }

        // Добавление нового рейтинга
        public void AddRating(Rating rating)
        {
            rating.EnsureUtc();
            _context.Ratings.Add(rating);  // Добавляем новый объект в контекст
            _context.SaveChanges();        // Сохраняем изменения в базе данных
        }

        // Получение рейтингов для конкретной игры
        public List<Rating> GetRatingsForGame(int gameId)
        {
            return _context.Ratings
                .Where(r => r.GameId == gameId)  // Фильтруем по gameId
                .ToList();                       // Преобразуем в список
        }
    }
}