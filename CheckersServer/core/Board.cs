using System;
using System.Collections.Generic;

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
