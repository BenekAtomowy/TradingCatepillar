using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using TradingCatepillar.Integration.GoogleGemini.Models;
using TradingCatepillar.Integration.GoogleGemini.Services.Interfaces;

namespace TradingCatepillar.Integration.GoogleGemini.Services
{
    public class GeminiHttpClient : IGeminiHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiHttpClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["ApiSettings:Gemini:ApiKey"] ?? throw new NullReferenceException("ApiSettings:Gemini:ApiKey");
        }

        public async Task<AIAnswer> Ask(AIPrompt instrumentPrompt)
        {
            var prompt = $"{instrumentPrompt.SystemInformation} {instrumentPrompt.PromptInformation} {instrumentPrompt.OutputDataFormat}";
            var result = await GenerateContentAsync(prompt);

            var json = result
                .Trim()
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();
            Console.WriteLine($"Gemini Response: {json}");
            return new AIAnswer(json);
        }
        

        private async Task<string> GenerateContentAsync(string prompt)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={_apiKey}";

            var payload = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
            };

            var json = JsonSerializer.Serialize(payload);
            var request = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, request);
            response.EnsureSuccessStatusCode();

            var sb = new StringBuilder();

            using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);

            try
            {
                var all = reader.ReadToEnd();
                using var doc = JsonDocument.Parse(all);
                var text = doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

                sb.Append(text);
            }
            catch
            {
                // Ignore error and return empty string
            }


            return sb.ToString();
        }
    }
}

