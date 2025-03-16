using Gamestudio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamestudio.Services
{
    public class ScoreServiceEF : IScoreService
    {
        private readonly CheckersGameContext _context;

        public ScoreServiceEF(CheckersGameContext context)
        {
            _context = context;
        }

        // Добавление нового счета
        public void AddScore(Score score)
        {
            score.EnsureUtc();
            _context.Scores.Add(score);  // Добавляем новый объект в контекст
            _context.SaveChanges();      // Сохраняем изменения в базе данных
        }

        // Получение топовых счетов
        public List<Score> GetTopScores(int top)
        {
            return _context.Scores
                .OrderByDescending(s => s.ScoreValue) // Сортируем по убыванию счета
                .Take(top)                            // Ограничиваем количество записей
                .ToList();                            // Преобразуем в список
        }

        // Получение счета по id
        public Score GetScoreById(int id)
        {
            return _context.Scores
                .FirstOrDefault(s => s.Id == id);  // Получаем первый элемент, если существует
        }
    }
}