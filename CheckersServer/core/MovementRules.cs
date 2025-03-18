using System;

namespace CheckersGame
{
    public class MovementRules
    {
        // Проверка на валидность хода
        public static bool IsValidMove(int startRow, int startCol, int endRow, int endCol, PieceColor color, Board board)
        {
            if (startRow < 0 || startRow >= 8 || startCol < 0 || startCol >= 8 || 
                endRow < 0 || endRow >= 8 || endCol < 0 || endCol >= 8)
            {
                return false;
            }

            Tile startTile = board.GetTile(startRow, startCol);
            Tile endTile = board.GetTile(endRow, endCol);

            if (startTile.Piece == null || startTile.Piece.Color != color)
            {
                return false; // Проверка на наличие своей фигуры на старте
            }

            bool isKing = startTile.Piece.IsKing;
            int direction = (color == PieceColor.White) ? -1 : 1; // Белые двигаются вверх, черные вниз

            // Проверка на обычный ход
            if (!isKing)
            {
                if (Math.Abs(startRow - endRow) == 1 && Math.Abs(startCol - endCol) == 1)
                {
                    if ((endRow - startRow) == direction && endTile.Piece == null)
                    {
                        PromoteToKingIfNeeded(endRow, startTile.Piece);
                        return true;
                    }
                }

                // Проверка на захват
                if (Math.Abs(startRow - endRow) == 2 && Math.Abs(startCol - endCol) == 2)
                {
                    int midRow = (startRow + endRow) / 2;
                    int midCol = (startCol + endCol) / 2;
                    Tile midTile = board.GetTile(midRow, midCol);

                    // Проверка на фигуру противника
                    if (midTile.Piece != null && midTile.Piece.Color != color && endTile.Piece == null)
                    {
                        PromoteToKingIfNeeded(endRow, startTile.Piece);
                        return true;
                    }
                }
            }
            else // Если фигура — король
            {
                if (Math.Abs(startRow - endRow) == Math.Abs(startCol - endCol))
                {
                    int rowDirection = (endRow > startRow) ? 1 : -1;
                    int colDirection = (endCol > startCol) ? 1 : -1;
                    int row = startRow + rowDirection;
                    int col = startCol + colDirection;
                    bool hasCaptured = false;

                    // Проверка на захват
                    while (row != endRow && col != endCol)
                    {
                        Tile currentTile = board.GetTile(row, col);
                        if (currentTile.Piece != null)
                        {
                            if (currentTile.Piece.Color == color || hasCaptured)
                            {
                                return false; // Если встретили свою фигуру или уже захватили, ход недопустим
                            }
                            hasCaptured = true; // Запоминаем, что был захват
                        }
                        row += rowDirection;
                        col += colDirection;
                    }
                    return endTile.Piece == null;
                }
            }

            return false;
        }

        // Метод для промоции фигуры в короля
        private static void PromoteToKingIfNeeded(int row, Piece piece)
        {
            if ((piece.Color == PieceColor.White && row == 0) || (piece.Color == PieceColor.Black && row == 7))
            {
                piece.IsKing = true;
            }
        }

        // Метод для проверки, может ли игрок сделать ход
        public static bool CanPlayerMove(PieceColor color, Board board)
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Tile tile = board.GetTile(row, col);
                    if (tile.Piece != null && tile.Piece.Color == color)
                    {
                        // Проверка всех возможных направлений для движения
                        for (int dr = -2; dr <= 2; dr += 2)
                        {
                            for (int dc = -2; dc <= 2; dc += 2)
                            {
                                if (IsValidMove(row, col, row + dr, col + dc, color, board))
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

        // Новый метод для проверки победы
        public static string CheckWinner(Board board)
        {
            bool whiteCanMove = CanPlayerMove(PieceColor.White, board);
            bool blackCanMove = CanPlayerMove(PieceColor.Black, board);

            if (!whiteCanMove && !blackCanMove)
            {
                return "Draw";  // Ничья, если оба игрока не могут сделать ход
            }
            else if (!whiteCanMove)
            {
                return "Black wins";  // Победа черных
            }
            else if (!blackCanMove)
            {
                return "White wins";  // Победа белых
            }

            return "Game in progress";  // Игра продолжается
        }
    }
}
