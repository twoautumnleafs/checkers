using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CheckersClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Загрузка настроек из appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string baseUrl = configuration["ApiSettings:BaseUrl"]; // Получаем базовый URL из настроек
            HttpClient client = new HttpClient();

            int selectedRow = -1, selectedCol = -1;

            while (true)
            {
                Console.Clear();
                await DisplayBoard(client, baseUrl); // Отображение текущей доски

                if (selectedRow != -1 && selectedCol != -1)
                {
                    Console.WriteLine($"Выбрана клетка: ({selectedRow}, {selectedCol})");
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                // Управление перемещением курсора
                int moveRow = 0, moveCol = 0;
                switch (keyInfo.Key)
                {
                    case ConsoleKey.W: // Вверх
                        if (selectedRow > 0) selectedRow--;
                        break;
                    case ConsoleKey.S: // Вниз
                        if (selectedRow < 7) selectedRow++;
                        break;
                    case ConsoleKey.A: // Влево
                        if (selectedCol > 0) selectedCol--;
                        break;
                    case ConsoleKey.D: // Вправо
                        if (selectedCol < 7) selectedCol++;
                        break;
                    case ConsoleKey.Spacebar: // Выбор фигуры
                        if (selectedRow != -1 && selectedCol != -1)
                        {
                            await MakeMove(client, baseUrl, selectedRow, selectedCol);
                        }
                        break;
                    case ConsoleKey.Escape: // Выход
                        return;
                    default:
                        break;
                }
            }
        }

        // Отображение доски
        private static async Task DisplayBoard(HttpClient client, string baseUrl)
        {
            var response = await client.GetAsync($"{baseUrl}/api/game/getBoard");
            if (response.IsSuccessStatusCode)
            {
                string board = await response.Content.ReadAsStringAsync();
                Console.WriteLine(board); // Отобразим доску в консоли
            }
            else
            {
                Console.WriteLine("Не удалось получить доску.");
            }
        }

        // Выполнение хода
        private static async Task MakeMove(HttpClient client, string baseUrl, int startRow, int startCol)
        {
            // Создайте объект запроса, в котором будет храниться информация о выбранной фигуре и ее движении
            var moveRequest = new
            {
                StartRow = startRow, // Позиция стартовой клетки
                StartCol = startCol, // Позиция стартовой клетки
                EndRow = startRow + 1, // Пример: движение на одну клетку вниз
                EndCol = startCol + 1  // Пример: движение на одну клетку вправо
            };

            string json = JsonConvert.SerializeObject(moveRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{baseUrl}/makeMove", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Ход выполнен.");
            }
            else
            {
                Console.WriteLine("Ошибка при выполнении хода.");
            }
        }
    }
}
