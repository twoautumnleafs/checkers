using CheckersGame;
using System;

namespace GameServer.Services
{
    public class CheckersService
    {
        private Game _currentGame;

        public CheckersService()
        {
            StartNewGame();
        }

        // Старт новой игры
        public Game StartNewGame()
        {
            _currentGame = new Game(new Player("Player 1", PieceColor.White), new Player("Player 2", PieceColor.Black));
            return _currentGame;
        }

        // Получение текущего состояния игры
        public GameState GetGameState()
        {
            if (_currentGame == null)
            {
                throw new InvalidOperationException("Игра не была начата.");
            }

            // Проверяем окончание игры и возвращаем состояние
            if (_currentGame.IsGameOver())
            {
                return GameState.Win; // Логика для победы, но может быть дополнена
            }

            return GameState.InProgress;
        }

        // Получение текущей доски игры
        public Board GetBoard()
        {
            if (_currentGame == null)
            {
                throw new InvalidOperationException("Игра не была начата.");
            }
            return _currentGame.GetBoard(); // Возвращаем текущую доску из игры
        }

        // Выполнение хода
        public MoveResult MakeMove(MoveRequest moveRequest)
        {
            if (_currentGame == null)
            {
                return new MoveResult { IsSuccess = false, ErrorMessage = "Игра не начата." };
            }

            bool success = _currentGame.MakeMove(moveRequest.StartRow, moveRequest.StartCol, moveRequest.EndRow, moveRequest.EndCol);
            
            return new MoveResult { IsSuccess = success, ErrorMessage = success ? null : "Недопустимый ход" };
        }
    }

}
