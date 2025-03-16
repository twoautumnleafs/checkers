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
    }
}