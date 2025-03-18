using Microsoft.AspNetCore.Mvc;
using GameServer.Models;
using GameServer.Services;
using System.Collections.Generic;
namespace GameServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("{game}")]
        public ActionResult<IEnumerable<Comment>> GetComments(string game)
        {
            return Ok(_commentService.GetComments(game));
        }

        [HttpPost]
        public ActionResult AddComment([FromBody] Comment comment)
        {
            _commentService.AddComment(comment);
            return NoContent();
        }
    }
}