namespace GameServer.Services
{
    public class MoveResult
    {
        public bool IsSuccess { get; set; }  // Успех или неудача хода
        public string ErrorMessage { get; set; }  // Сообщение об ошибке, если ход неудачен
    }
}