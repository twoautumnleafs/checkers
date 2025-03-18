using GameServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly CheckersService _checkersService;

        public GameController(CheckersService checkersService)
        {
            _checkersService = checkersService;
        }

        // Получить текущую доску
        [HttpGet("getBoard")]
        public IActionResult GetBoard()
        {
            var board = _checkersService.GetBoard();  // Получаем доску
            var serializedBoard = board.GetSerializedBoard();  // Сериализуем доску
            return Ok(serializedBoard);
        }

        // Получить состояние игры
        [HttpGet("getGameState")]
        public IActionResult GetGameState()
        {
                var gameState = _checkersService.GetGameState();  // Получаем состояние игры
                return Ok(gameState);
        }

        // Старт новой игры
        [HttpPost("startNewGame")]
        public IActionResult StartNewGame()
        {
                var newGame = _checkersService.StartNewGame();  // Стартуем новую игру
                return Ok(newGame);  // Возвращаем данные новой игры
        }

        // Выполнить ход
        [HttpPost("makeMove")]
        public IActionResult MakeMove([FromBody] MoveRequest moveRequest)
        {
            if (moveRequest == null)
            {
                return BadRequest("Неверные данные хода.");
            }
            
                var result = _checkersService.MakeMove(moveRequest);  // Выполняем ход
                if (result.IsSuccess)
                {
                    return Ok("Ход выполнен успешно.");
                }
                else
                {
                    return BadRequest(result.ErrorMessage);  // Возвращаем ошибку, если ход неудачный
                }
        }
    }
}
