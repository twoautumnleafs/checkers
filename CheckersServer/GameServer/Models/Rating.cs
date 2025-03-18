using System;
namespace GameServer.Models;

public class Rating
{
    public int Id { get; set; }
    public string Game { get; set; } = default!;
    public string Player { get; set; } = default!;
    public int Value { get; set; }
    public DateTime RatedAt { get; set; }
}
