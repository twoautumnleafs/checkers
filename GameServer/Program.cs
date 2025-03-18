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

// Добавление контекста для работы с базой данных
builder.Services.AddDbContext<GameContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация других сервисов
builder.Services.AddScoped<ScoreService>();
builder.Services.AddScoped<RatingService>();
builder.Services.AddScoped<CommentService>();

// Регистрация CheckersService
builder.Services.AddScoped<CheckersService>();  // Это нужно добавить

// Добавление контроллеров
builder.Services.AddControllers();

var app = builder.Build();

// Настройка маршрутов
app.MapControllers();

// Запуск приложения
app.Run();