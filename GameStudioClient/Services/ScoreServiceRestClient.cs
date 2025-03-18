using GameStudioClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameStudioClient.Services
{
    public class ScoreServiceRestClient : IScoreService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5000/api/Score";

        public ScoreServiceRestClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Score>> GetTopScoresAsync(string gameName)
        {
            var response = await _httpClient.GetStringAsync($"{_baseUrl}/{gameName}");
            return JsonConvert.DeserializeObject<List<Score>>(response);
        }

        public async Task AddScoreAsync(Score score)
        {
            var jsonContent = JsonConvert.SerializeObject(score);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_baseUrl, content);
        }
    }
}