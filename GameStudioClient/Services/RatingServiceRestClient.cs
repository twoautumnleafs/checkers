using GameStudioClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameStudioClient.Services
{
    public class RatingServiceRestClient : IRatingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5000/api/Rating";

        public RatingServiceRestClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Rating>> GetRatingsAsync(string gameName)
        {
            var response = await _httpClient.GetStringAsync($"{_baseUrl}/{gameName}");
            return JsonConvert.DeserializeObject<List<Rating>>(response);
        }

        public async Task AddRatingAsync(Rating rating)
        {
            var jsonContent = JsonConvert.SerializeObject(rating);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_baseUrl, content);
        }
    }
}