using Microsoft.AspNetCore.Mvc;
using GameServer.Services;
using GameServer.Models;
using System;
namespace GameServer.Controllers
{
    [Route("api/checkers")]
    [ApiController]
    public class CheckersController : ControllerBase
    {
        private readonly CheckersService _checkersService;

        public CheckersController(CheckersService checkersService)
        {
            _checkersService = checkersService;
        }

        // Старт новой игры
        [HttpPost("startNewGame")]
        public IActionResult StartNewGame()
        {
            try
            {
                var result = _checkersService.StartNewGame();
                if (result)
                {
                    return Ok("Новая игра начата");
                }
                else
                {
                    return BadRequest("Не удалось начать новую игру.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при запуске новой игры: {ex.Message}");
            }
        }

        // Получить текущую доску
        [HttpGet("getBoard")]
        public ActionResult<string> GetSerializedBoard()
        {
            // Вызов метода GetSerializedBoard, чтобы получить сериализованное состояние доски
            var serializedBoard = _checkersService.GetSerializedBoard();
            return Ok(serializedBoard);  // Возвращаем сериализованную доску в формате JSON
        }
        // Получить состояние игры
        [HttpPost("getGameState")]
        public IActionResult GetGameState()
        {
            try
            {
                var gameState = _checkersService.GetGameState();  // Получаем состояние игры
                return Ok(gameState);  // Возвращаем данные напрямую, если структура подходит
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при получении состояния игры: {ex.Message}");
            }
        }

        // Выполнить ход
        [HttpPost("makeMove")]
        public IActionResult MakeMove([FromBody] MoveRequest moveRequest)
        {
            if (moveRequest == null)
            {
                return BadRequest(new { message = "Неверные данные хода." });
            }

            try
            {
                var result = _checkersService.MakeMove(moveRequest);  // Выполняем ход
                if (result.IsSuccess)
                {
                    return Ok(new { message = "Ход выполнен успешно." });
                }
                else
                {
                    return BadRequest(new { message = result.ErrorMessage });  // Возвращаем ошибку, если ход неудачный
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ошибка при выполнении хода: {ex.Message}" });
            }
        }

        // Получить текущего игрока
        [HttpGet("getCurrentPlayer")]
        public IActionResult GetCurrentPlayer()
        {
            try
            {
                var currentPlayer = _checkersService.CurrentPlayer;  // Получаем текущего игрока
                return Ok(new { currentPlayerName = currentPlayer.Name, currentPlayerColor = currentPlayer.Color.ToString() });
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при получении текущего игрока: {ex.Message}");
            }
        }

        // Получить победителя игры
        [HttpGet("getWinner")]
        public IActionResult GetWinner()
        {
            try
            {
                var winner = _checkersService.GetGameState();  // Получаем состояние игры
                return Ok(new { winner });
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка при проверке победителя: {ex.Message}");
            }
        }
    }
}
