namespace CheckersGame
{
    public class Tile
    {
        public Piece Piece { get; set; }

        public Tile(Piece piece)
        {
            Piece = piece;
        }
    }
}
