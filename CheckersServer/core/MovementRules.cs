using System;

namespace CheckersGame
{
    public class MovementRules
    {
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
                return false;
            }

            bool isKing = startTile.Piece.IsKing;
            int direction = (color == PieceColor.White) ? -1 : 1; // Белые двигаются вверх, черные вниз

            if (!isKing)
            {
                // Обычное движение вперёд
                if (Math.Abs(startRow - endRow) == 1 && Math.Abs(startCol - endCol) == 1)
                {
                    if ((endRow - startRow) == direction && endTile.Piece == null)
                    {
                        PromoteToKingIfNeeded(endRow, startTile.Piece);
                        return true;
                    }
                }
                
                // Захват (вперед и назад разрешено)
                if (Math.Abs(startRow - endRow) == 2 && Math.Abs(startCol - endCol) == 2)
                {
                    int midRow = (startRow + endRow) / 2;
                    int midCol = (startCol + endCol) / 2;
                    Tile midTile = board.GetTile(midRow, midCol);
                    
                    if (midTile.Piece != null && midTile.Piece.Color != color && endTile.Piece == null)
                    {
                        PromoteToKingIfNeeded(endRow, startTile.Piece);
                        return true;
                    }
                }
            }
            else
            {
                if (Math.Abs(startRow - endRow) == Math.Abs(startCol - endCol))
                {
                    int rowDirection = (endRow > startRow) ? 1 : -1;
                    int colDirection = (endCol > startCol) ? 1 : -1;
                    int row = startRow + rowDirection;
                    int col = startCol + colDirection;
                    bool hasCaptured = false;

                    while (row != endRow && col != endCol)
                    {
                        Tile currentTile = board.GetTile(row, col);
                        if (currentTile.Piece != null)
                        {
                            if (currentTile.Piece.Color == color || hasCaptured)
                            {
                                return false;
                            }
                            hasCaptured = true;
                        }
                        row += rowDirection;
                        col += colDirection;
                    }
                    return endTile.Piece == null;
                }
            }
            
            return false;
        }

        private static void PromoteToKingIfNeeded(int row, Piece piece)
        {
            if ((piece.Color == PieceColor.White && row == 0) || (piece.Color == PieceColor.Black && row == 7))
            {
                piece.IsKing = true;
            }
        }
    }
}
