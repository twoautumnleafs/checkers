using System;
namespace GameServer.Models;

public class Comment
{
    public int Id { get; set; }
    public string Game { get; set; } = default!;
    public string Player { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime CommentedAt { get; set; }
}
