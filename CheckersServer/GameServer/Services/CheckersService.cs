using GameServer.Models;
using Newtonsoft.Json;  // Используем Newtonsoft.Json для сериализации
using System;
using System.Collections.Generic;

namespace GameServer.Services
{
    public class CheckersService
    {
        private CheckersGame _game;

        public CheckersService()
        {
            _game = new CheckersGame();
        }

        // Метод для старта новой игры
        public bool StartNewGame()
        {
            _game = new CheckersGame();  // Создаем новую игру
            return true;  // Возвращаем успех
        }

        // Метод для получения доски игры
        public Board GetBoard()
        {
            return _game.Board;  // Возвращаем текущую доску
        }

        // Метод для получения состояния игры (например, кто выиграл)
        public string GetGameState()
        {
            if (_game.IsGameOver)
            {
                return "Игра завершена.";
            }

            // Если игра не завершена, возвращаем, кто сейчас ходит
            return _game.CurrentPlayer.Name + " ходит.";
        }

        // Метод для выполнения хода
        public (bool IsSuccess, string ErrorMessage) MakeMove(MoveRequest moveRequest)
        {
            // Пример логики выполнения хода
            try
            {
                var from = new Position(moveRequest.FromRow, moveRequest.FromCol);
                var to = new Position(moveRequest.ToRow, moveRequest.ToCol);

                if (!_game.MakeMove(from, to))
                {
                    return (false, "Невозможный ход.");
                }

                return (true, string.Empty);  // Ход успешен
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка при выполнении хода: {ex.Message}");
            }
        }

        // Метод для получения сериализованной доски
        public string GetSerializedBoard()
        {
            // Сериализуем доску игры в JSON формат
            return JsonConvert.SerializeObject(_game.Board);
        }

        // Свойство для получения текущего игрока
        public Player CurrentPlayer => _game.CurrentPlayer;
    }

    // Модели игры (просто для примера)
    public class CheckersGame
    {
        public Board Board { get; private set; }
        public Player CurrentPlayer { get; private set; }
        public bool IsGameOver { get; private set; }

        public CheckersGame()
        {
            Board = new Board();
            CurrentPlayer = new Player("Игрок 1", PlayerColor.White);
            IsGameOver = false;
        }

        // Метод для выполнения хода
        public bool MakeMove(Position from, Position to)
        {
            // Пример выполнения хода
            if (IsValidMove(from, to))
            {
                // Логика хода (упрощенная)
                Board.MovePiece(from, to);
                SwitchPlayer();
                return true;
            }
            return false;
        }

        private bool IsValidMove(Position from, Position to)
        {
            // Упрощенная проверка на возможность хода
            return true;
        }

        private void SwitchPlayer()
        {
            // Логика переключения игроков
            CurrentPlayer = CurrentPlayer.Color == PlayerColor.White ? new Player("Игрок 2", PlayerColor.Black) : new Player("Игрок 1", PlayerColor.White);
        }
    }

    public class Board
    {
        public void MovePiece(Position from, Position to)
        {
            // Логика перемещения фигуры на доске
        }
    }

    public class Player
    {
        public string Name { get; }
        public PlayerColor Color { get; }

        public Player(string name, PlayerColor color)
        {
            Name = name;
            Color = color;
        }
    }

    public enum PlayerColor
    {
        White,
        Black
    }

    public class Position
    {
        public int Row { get; }
        public int Col { get; }

        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
