namespace CheckersGame
{
    public class Piece
    {
        public PieceColor Color { get; set; }
        public bool IsKing { get; set; }

        public Piece(PieceColor color)
        {
            Color = color;
            IsKing = false; // Изначально фигура не является дамкой
        }

        public void Crown()
        {
            IsKing = true; // Делает фигуру дамкой
        }

        // Метод для строкового представления фигуры
        public string ToStringRepresentation()
        {
            if (IsKing)
            {
                return Color == PieceColor.White ? "W" : "B";  // "W" для дамки белых, "B" для дамки черных
            }
            return Color == PieceColor.White ? "w" : "b";  // "w" для обычной белой, "b" для обычной черной
        }
    }
}