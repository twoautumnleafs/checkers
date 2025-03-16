using System;

namespace CheckersGame
{
    public class GameConsole
    {
        private Game game;
        private Player player1;
        private Player player2;
        private int selectedRow = 0;
        private int selectedCol = 0;
        private bool pieceSelected = false;
        private int pieceRow, pieceCol;
        private int movesCount = 0;

        public GameConsole()
        {
            player1 = new Player("Player 1", PieceColor.White);
            player2 = new Player("Player 2", PieceColor.Black);
            game = new Game(player1, player2);
        }

        public void DisplayBoard()
        {
            Console.Clear();
            Console.WriteLine(new string('\n', 50));

            Console.Write("   ");
            for (int col = 0; col < 8; col++)
            {
                Console.Write(col + " ");
            }
            Console.WriteLine();

            for (int row = 0; row < 8; row++)
            {
                Console.Write(row + "  ");
                for (int col = 0; col < 8; col++)
                {
                    Tile tile = game.GetBoard().GetTile(row, col);
                    string pieceChar = ".";
                    if (tile.Piece != null)
                    {
                        pieceChar = tile.Piece.Color == PieceColor.White ? "a" : "b";
                        if (tile.Piece.IsKing)
                            pieceChar = pieceChar.ToUpper();
                    }
            
                    if (row == selectedRow && col == selectedCol)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write(pieceChar + " ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            // Display the moves count at the bottom
            Console.WriteLine();
            Console.WriteLine($"Moves count: {movesCount}");
        }

        public bool HandleInput(Player player)
{
    ConsoleKey key = Console.ReadKey(true).Key;
    switch (key)
    {
        case ConsoleKey.W:
            if (selectedRow > 0) selectedRow--;
            break;
        case ConsoleKey.S:
            if (selectedRow < 7) selectedRow++;
            break;
        case ConsoleKey.A:
            if (selectedCol > 0) selectedCol--;
            break;
        case ConsoleKey.D:
            if (selectedCol < 7) selectedCol++;
            break;
        case ConsoleKey.Spacebar:
            if (!pieceSelected)
            {
                Tile tile = game.GetBoard().GetTile(selectedRow, selectedCol);
                if (tile.Piece != null && tile.Piece.Color == player.Color)
                {
                    pieceRow = selectedRow;
                    pieceCol = selectedCol;
                    pieceSelected = true;
                }
            }
            else
            {
                bool moveResult = game.MakeMove(pieceRow, pieceCol, selectedRow, selectedCol);
                if (moveResult)
                {
                    // Проверяем, стала ли фишка королевой
                    Tile tile = game.GetBoard().GetTile(selectedRow, selectedCol);
                    if (tile.Piece != null)
                    {
                        if (player.Color == PieceColor.White && selectedRow == 0)
                        {
                            tile.Piece.IsKing = true;
                        }
                        else if (player.Color == PieceColor.Black && selectedRow == 7)
                        {
                            tile.Piece.IsKing = true;
                        }
                    }

                    pieceSelected = false;  // Сбрасываем флаг выбора фишки после выполнения хода
                    movesCount++;
                    return true;  // Возвращаем, чтобы завершить ход
                }
                else
                {
                    // Если ход не удался, сбрасываем выбор фишки, и даем возможность выбрать другую фишку
                    pieceSelected = false;
                }
            }
            break;
    }
    DisplayBoard();
    return false;
}

        public void Play()
        {
            DisplayBoard();
            while (true)
            {
                if (HandleInput(player1))
                {
                    DisplayBoard();
                    if (game.IsGameOver()) break;
                }
                
                if (HandleInput(player2))
                {
                    DisplayBoard();
                    if (game.IsGameOver()) break;
                }
            }
            Console.WriteLine("Игра завершена!");
        }
    }
}