namespace CheckersGame
{
    public class Player
    {
        public string Name { get; set; }
        public PieceColor Color { get; set; }

        public Player(string name, PieceColor color)
        {
            Name = name;
            Color = color;
        }
    }
}
