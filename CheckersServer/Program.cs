using Microsoft.EntityFrameworkCore;
using GameServer.Data;
using GameServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Http;
using System;
var builder = WebApplication.CreateBuilder(args);

// Получаем конфигурацию
var configuration = builder.Configuration;

// Добавляем контекст базы данных с получением строки подключения
builder.Services.AddDbContext<GameContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Регистрация сервисов
builder.Services.AddScoped<ScoreService>();
builder.Services.AddScoped<RatingService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<CheckersService>();

// Добавление контроллеров
builder.Services.AddControllersWithViews();

// Добавление CORS (если нужно)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5000")  // адрес фронтенда (например, если это React)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Добавление Swagger (если нужно)
builder.Services.AddSwaggerGen();

// Если используется сессия, добавьте
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Пример настройки тайм-аута сессии
});

// Если аутентификация или авторизация, добавьте это
// builder.Services.AddAuthentication(...);
// builder.Services.AddAuthorization(...);

var app = builder.Build();

// Использование CORS
app.UseCors();

// Раздача статических файлов из wwwroot
app.UseStaticFiles();

// Настройка маршрута для главной страницы
app.MapGet("/", () =>
{
    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html");

    if (File.Exists(filePath))
    {
        return Results.File(filePath, "text/html");
    }
    else
    {
        return Results.NotFound();
    }
});

// Если нужно использовать Swagger для API
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Checkers API V1");
    c.RoutePrefix = string.Empty;
});

// Настройка маршрутов для контроллеров
app.MapControllers();

// Запуск приложения
app.Run();
