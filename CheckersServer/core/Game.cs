using System;

namespace CheckersGame
{
    public class Game
    {
        private Board board;
        private Player currentPlayer;

        public Game(Player player1, Player player2)
        {
            board = new Board();  // Инициализируем доску
            currentPlayer = player1; // Изначально ходит первый игрок
            PrintBoard();
        }

        // Метод для получения доски
        public Board GetBoard()
        {
            return board;
        }

        // Метод для печати доски
        public void PrintBoard()
        {
            // Получаем сериализованную доску
            var serializedBoard = board.GetSerializedBoard();

            // Для каждой строки (ряда) доски
            foreach (var row in serializedBoard)
            {
                // Печатаем каждую клетку строки с пробелом между ними
                Console.WriteLine(string.Join(" ", row));
            }
        }



        public bool MakeMove(int startRow, int startCol, int endRow, int endCol)
        {
            // Проверка на валидность хода
            if (!MovementRules.IsValidMove(startRow, startCol, endRow, endCol, currentPlayer.Color, board))
            {
                return false; // Ход не валиден
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

            // Перемещаем фигуру на новое место
            endTile.Piece = startTile.Piece;
            startTile.Piece = null;

            // Меняем игрока
            currentPlayer = (currentPlayer.Color == PieceColor.White) ? new Player("Player 2", PieceColor.Black) : new Player("Player 1", PieceColor.White);
            return true;
        }

        // Получение текущего игрока
        public Player CurrentPlayer => currentPlayer;

        // Метод для проверки завершения игры
        public bool IsGameOver()
        {
            return !CanPlayerMove(currentPlayer);  // Если игрок не может сделать ход, игра завершена
        }

        // Метод для проверки, может ли игрок сделать ход
        private bool CanPlayerMove(Player player)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Tile tile = board.GetTile(row, col);
                    if (tile.Piece != null && tile.Piece.Color == player.Color)
                    {
                        // Проверка всех возможных направлений для движения
                        for (int dr = -2; dr <= 2; dr += 2)
                        {
                            for (int dc = -2; dc <= 2; dc += 2)
                            {
                                if (MovementRules.IsValidMove(row, col, row + dr, col + dc, player.Color, board))
                                {
                                    return true;  // Если хотя бы один возможный ход есть, возвращаем true
                                }
                            }
                        }
                    }
                }
            }
            return false;  // Если нет доступных ходов, возвращаем false
        }
    }
}
