using System;
namespace CheckersGame
{
    public class Game
    {
        private Board board;
        private Player currentPlayer;

        public Game(Player player1, Player player2)
        {
            board = new Board();
            currentPlayer = player1; // Изначально ходит первый игрок
        }

        // Метод для получения доски
        public Board GetBoard()
        {
            return board;
        }

        // Метод для проверки окончания игры
        public bool IsGameOver()
        {
            // Простейшая логика завершения игры (например, если нет доступных ходов у одного из игроков)
            // Здесь можно добавить логику для проверки наличия победителя или ничьей
            // Например, если один из игроков не может сделать ход, игра закончена
            return false; // В данный момент просто заглушка
        }
        
        public bool MakeMove(int startRow, int startCol, int endRow, int endCol)
        {
            // Проверка на валидность хода (включая захват)
            if (!MovementRules.IsValidMove(startRow, startCol, endRow, endCol, currentPlayer.Color, board))
            {
                return false;
            }

            Tile startTile = board.GetTile(startRow, startCol);
            Tile endTile = board.GetTile(endRow, endCol);

            // Если это захват, удаляем фигуру противника
            if (Math.Abs(startRow - endRow) == 2 && Math.Abs(startCol - endCol) == 2)
            {
                int midRow = (startRow + endRow) / 2;
                int midCol = (startCol + endCol) / 2;

                // Удаляем фигуру противника с промежуточной клетки
                board.GetTile(midRow, midCol).Piece = null;
            }

            // Перемещаем фигуру
            endTile.Piece = startTile.Piece;
            startTile.Piece = null;

            // Меняем игрока
            currentPlayer = (currentPlayer.Color == PieceColor.White) ? new Player("Player 2", PieceColor.Black) : new Player("Player 1", PieceColor.White);
            return true;
        }
    }
}
