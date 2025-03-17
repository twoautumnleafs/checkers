using Microsoft.EntityFrameworkCore;
using GameServer.Data;
using GameServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GameContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ScoreService>();
builder.Services.AddScoped<RatingService>();
builder.Services.AddScoped<CommentService>();

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();