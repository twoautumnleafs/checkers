using GameStudioClient.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameStudioClient.Services
{
    public class CommentServiceRestClient : ICommentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5000/api/Comment";

        public CommentServiceRestClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Comment>> GetCommentsAsync(string gameName)
        {
            var response = await _httpClient.GetStringAsync($"{_baseUrl}/{gameName}");
            return JsonConvert.DeserializeObject<List<Comment>>(response);
        }

        public async Task AddCommentAsync(Comment comment)
        {
            var jsonContent = JsonConvert.SerializeObject(comment);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(_baseUrl, content);
        }
    }
}