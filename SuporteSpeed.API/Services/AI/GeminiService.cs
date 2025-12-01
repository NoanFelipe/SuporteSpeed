using System.Text;
using System.Text.Json;

namespace SuporteSpeed.API.Services.AI
{
    public class GeminiService : IGeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string ModelId = "gemini-1.5-flash";

        public GeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GeminiApiKey"];

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new ArgumentNullException("GeminiApiKey", "A chave de API do Gemini não foi encontrada nas configurações.");
            }
        }

        public async Task<string> GenerateTicketResponseAsync(string title, string field, string description)
        {
            var prompt = $"Você é um agente de suporte técnico experiente da empresa SuporteSpeed. " +
                         $"Por favor, responda ao seguinte chamado de forma cordial, técnica e resolutiva.\n\n" +
                         $"Dados do Chamado:\n" +
                         $"- Título: {title}\n" +
                         $"- Setor (Field): {field}\n" +
                         $"- Descrição dada pelo Usuário: {description}\n\n" +
                         $"Gere apenas a resposta que deve ser enviada ao usuário. A resposta deve ser curta e direto ao ponto.";

            var requestBody = new GeminiRequest
            {
                Contents = new List<GeminiContent>
                {
                    new GeminiContent
                    {
                        Parts = new List<GeminiPart>
                        {
                            new GeminiPart { Text = prompt }
                        }
                    }
                }
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                Encoding.UTF8,
                "application/json");

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/{ModelId}:generateContent?key={_apiKey}";

            var response = await _httpClient.PostAsync(url, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return $"Erro ao comunicar com a IA: {response.StatusCode} - {errorContent}";
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(
                responseString,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var textResponse = geminiResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text;

            return textResponse ?? "A IA não conseguiu gerar uma resposta válida para este ticket.";
        }
    }
}
