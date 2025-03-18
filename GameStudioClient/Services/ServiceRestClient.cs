using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GameStudioClient.Services
{
    public class GameServiceRestClient
    {
        private readonly HttpClient _httpClient;

        public GameServiceRestClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> MakeMoveAsync(string playerColor, string direction)
        {
            var moveRequest = new
            {
                PlayerColor = playerColor,
                Direction = direction
            };

            var json = JsonSerializer.Serialize(moveRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("http://localhost:5000/api/game/move", content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetBoardAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5000/api/game/board");
            return await response.Content.ReadAsStringAsync();
        }
    }
}