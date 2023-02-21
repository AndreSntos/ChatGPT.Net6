using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Options;
using System.Runtime;
using ChatGPT.Models;

namespace ChatGPT.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly AppSettings _settings;
        private readonly HttpClient _httpClient;
        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        public OpenAIService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(settings.Value.ChatGPT.OpenAIUrl);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", settings.Value.ChatGPT.OpenAIToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> AskQuestion(string query)
        {
            var completion = new CompletionRequest()
            {
                Prompt = query
            };

            var body = JsonSerializer.Serialize(completion);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("v1/completions", content);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<CompletionResponse>(options);
                return data?.Choices?.FirstOrDefault().Text;
            }

            return default;
        }
    }
}