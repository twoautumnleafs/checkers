using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace GameServer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Путь к файлу должен быть абсолютным
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html");

            // Если файл существует, отправить его
            if (System.IO.File.Exists(filePath))
            {
                return PhysicalFile(filePath, "text/html");
            }
            else
            {
                return NotFound(); // Если файл не найден, вернуть 404
            }
        }
    }
}