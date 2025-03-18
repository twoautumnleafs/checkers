using Microsoft.AspNetCore.Mvc;
using GameServer.Models;
using GameServer.Services;
using System.Collections.Generic;
namespace GameServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly ScoreService _scoreService;

        public ScoreController(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        [HttpGet("{game}")]
        public ActionResult<IEnumerable<Score>> GetTopScores(string game)
        {
            var scores = _scoreService.GetTopScores(game);
            return Ok(scores);
        }

        [HttpPost]
        public ActionResult AddScore([FromBody] Score score)
        {
            _scoreService.AddScore(score);
            return NoContent();
        }
    }
}