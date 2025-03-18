using System;
using CheckersGame;
using GameServer.Models;
namespace CheckersGame
{
    public class MoveValidator
    {
        public bool IsValidMove(Board board, MoveRequest move)
        {
            // Проверяем, находятся ли координаты в пределах доски
            if (!IsWithinBounds(move.FromRow, move.FromCol) || !IsWithinBounds(move.ToRow, move.ToCol))
            {
                return false;
            }

            var fromTile = board.GetTile(move.FromRow, move.FromCol);
            var toTile = board.GetTile(move.ToRow, move.ToCol);

            // Проверяем, есть ли шашка на начальной клетке и пуста ли конечная
            if (fromTile.Piece == null || toTile.Piece != null)
            {
                return false;
            }

            // Проверяем корректность движения (например, ход по диагонали на одну клетку)
            int rowDiff = Math.Abs(move.ToRow - move.FromRow);
            int colDiff = Math.Abs(move.ToCol - move.FromCol);

            return rowDiff == 1 && colDiff == 1; // Базовое правило для обычного хода
        }

        private bool IsWithinBounds(int row, int col)
        {
            return row >= 0 && row < 8 && col >= 0 && col < 8;
        }
    }
}