using System;
using System.Collections.Generic;
using GameServer.Models;
namespace CheckersGame
{
    public class Board
    {
        private Tile[,] board;

        public Board()
        {
            board = new Tile[8, 8];
            InitializeBoard();
        }
        public bool ApplyMove(MoveRequest move)
        {
            var fromTile = board[move.FromRow, move.FromCol];
            var toTile = board[move.ToRow, move.ToCol];

            if (fromTile.Piece == null || toTile.Piece != null)
            {
                return false; // Невозможный ход
            }

            toTile.Piece = fromTile.Piece;
            fromTile.Piece = null;

            return true;
        }
        private void InitializeBoard()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if ((row + col) % 2 == 0)
                    {
                        if (row < 3)
                        {
                            board[row, col] = new Tile(new Piece(PieceColor.Black));
                        }
                        else if (row > 4)
                        {
                            board[row, col] = new Tile(new Piece(PieceColor.White));
                        }
                        else
                        {
                            board[row, col] = new Tile(null);
                        }
                    }
                    else
                    {
                        board[row, col] = new Tile(null);
                    }
                }
            }
        }

        public Tile GetTile(int row, int col)
        {
            return board[row, col];
        }

        public void SetTile(int row, int col, Tile tile)
        {
            board[row, col] = tile;
        }

        // Метод для сериализации доски
        public List<string> GetSerializedBoard()
        {
            var serializedBoard = new List<string>();

            for (int row = 0; row < 8; row++)
            {
                var rowString = "";
                for (int col = 0; col < 8; col++)
                {
                    var tile = board[row, col];
                    if (tile.Piece != null)
                    {
                        rowString += tile.Piece.Color == PieceColor.White ? " w " : " b ";
                    }
                    else
                    {
                        rowString += " . "; // Пустая клетка
                    }
                }
                serializedBoard.Add(rowString);
            }

            return serializedBoard;
        }

    }
}
