using Microsoft.AspNetCore.Mvc;
using GameServer.Models;
using GameServer.Services;
using System.Collections.Generic;
namespace GameServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly RatingService _ratingService;

        public RatingController(RatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Rating>> GetRatings()
        {
            return Ok(_ratingService.GetAllRatings());
        }

        [HttpPost]
        public ActionResult AddRating([FromBody] Rating rating)
        {
            _ratingService.AddRating(rating);
            return NoContent();
        }
    }
}