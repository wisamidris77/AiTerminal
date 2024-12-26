using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AiTerminal.Abstraction;
using AiTerminal.Models;
using AiTerminal.Providers;

namespace AiTerminal.Clients
{

    public class GeminiClient : IAiClient
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private const string GeminiApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash-exp:generateContent";
        public GeminiClient(string apiKey)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("x-goog-api-key", _apiKey);
        }


        public async Task<string> GenerateContent(ChatContents contents, ContentModel systemInstructions)
        {
            var requestData = new
            {
                contents = contents.ContentModels,
                systemInstruction = systemInstructions,
                generationConfig = new
                {
                    temperature = 1,
                    topK = 40,
                    topP = 0.95,
                    maxOutputTokens = 8192,
                    responseMimeType = "text/plain"
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestData, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }), Encoding.UTF8, "application/json");

            try
            {
                using var response = await _httpClient.PostAsync($"{GeminiApiUrl}?key={_apiKey}", content);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonSerializer.Deserialize<GeminiResponse>(responseString, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

                var textParts = jsonResponse.Candidates.First().Content.Parts;

                StringBuilder fullResponse = new StringBuilder();
                foreach (var part in textParts)
                {
                    if (part.Text != null)
                    {
                        fullResponse.Append(part.Text);
                    }
                }

                return fullResponse.ToString();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error communicating with Gemini API: {ex.Message}");
                throw;
            }
        }

        public class GeminiResponse
        {
            public List<CandidateModel> Candidates { get; set; }
        }
    }
}
