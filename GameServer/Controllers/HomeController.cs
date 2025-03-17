using Microsoft.AspNetCore.Mvc;

namespace GameServer.Controllers
{
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to the Game Server API!");
        }
    }
}