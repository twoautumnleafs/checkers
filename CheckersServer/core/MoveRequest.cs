namespace GameServer.Services
{
    public class MoveRequest
    {
        public int StartRow { get; set; }  // Начальная строка (ряд)
        public int StartCol { get; set; }  // Начальный столбец
        public int EndRow { get; set; }    // Конечная строка (ряд)
        public int EndCol { get; set; }    // Конечный столбец
    }
}