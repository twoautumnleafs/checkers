using System;
namespace GameServer.Models;

public class Score
{
    public int Id { get; set; }
    public string Game { get; set; } = default!;
    public string Player { get; set; } = default!;
    public int Points { get; set; }
    public DateTime PlayedAt { get; set; }
}
