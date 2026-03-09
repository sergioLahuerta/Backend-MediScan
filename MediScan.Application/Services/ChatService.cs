using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using MediScan.Core.Interfaces.Services;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Entities;
using MediScan.Core.Enums;

namespace MediScan.Application.Services;

public class ChatService : IChatService
{
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _groqApiKey;

    public ChatService(IChatMessageRepository chatMessageRepository, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _chatMessageRepository = chatMessageRepository;
        _httpClientFactory = httpClientFactory;
        
        // Api Key de appsettings.json
        _groqApiKey = configuration["Groq:ApiKey"] 
                     ?? "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";
    }

    public async Task<string> ProcessChatAsync(string sessionId, string userMessage, string? base64Image)
    {
        var sessionGuid = Guid.Empty;
        Guid.TryParse(sessionId, out sessionGuid);

        try
        {
            // Guardado del mensaje del usuario en SQL
            if (sessionGuid != Guid.Empty)
            {
                await _chatMessageRepository.AddAsync(new ChatMessage
                {
                    ChatSessionId = sessionGuid,
                    SenderType = SenderType.User,
                    MessageText = userMessage,
                    SentAt = DateTime.UtcNow
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DB ERROR - User Message]: {ex.Message}");
        }

        // Respuesta de la IA (pasando la imagen si existe)
        string aiResponse = await GetAIResponse(userMessage, base64Image);

        try
        {
            // Guardado de la respuesta de la IA en SQL
            if (sessionGuid != Guid.Empty)
            {
                await _chatMessageRepository.AddAsync(new ChatMessage
                {
                    ChatSessionId = sessionGuid,
                    SenderType = SenderType.AI,
                    MessageText = aiResponse,
                    SentAt = DateTime.UtcNow
                });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DB ERROR - AI Response]: {ex.Message}");
        }

        return aiResponse;
    }

    public async Task<string> GenerateClinicalReportAsync(string sessionId)
    {
        if (!Guid.TryParse(sessionId, out var sessionGuid))
            return "ID de sesión inválido.";

        var messages = await _chatMessageRepository.GetMessagesBySessionIdAsync(sessionGuid);
        if (messages == null || !messages.Any())
            return "No hay suficientes datos en la conversación para generar un informe.";

        // Construir el historial para la IA
        var historyBuilder = new StringBuilder();
        foreach (var msg in messages.OrderBy(m => m.SentAt))
        {
            historyBuilder.AppendLine($"{(msg.SenderType == SenderType.User ? "Paciente" : "IA")}: {msg.MessageText}");
        }

        string prompt = "Basado en la siguiente conversación médica, genera un informe clínico estructurado y profesional.\n" +
                        "EL INFORME DEBE TENER ESTAS SECCIONES:\n" +
                        "### 1. Resumen de Sintomatología\n" +
                        "### 2. Diagnóstico Diferencial Estimado\n" +
                        "### 3. Plan de Acción y Tratamiento Sugerido\n" +
                        "### 4. Recomendaciones Adicionales y Signos de Alerta\n\n" +
                        "CONVERSACIÓN:\n" +
                        historyBuilder.ToString();

        return await GetAIResponse(prompt);
    }

    private async Task<string> GetAIResponse(string prompt, string? base64Image = null)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var url = "https://api.groq.com/openai/v1/chat/completions";

            var userContent = new List<object> { new { type = "text", text = prompt } };

            if (!string.IsNullOrEmpty(base64Image))
            {
                userContent.Add(new { 
                    type = "image_url", 
                    image_url = new { url = $"data:image/jpeg;base64,{base64Image}" } 
                });
            }

            var requestBody = new
            {
                model = "meta-llama/llama-4-scout-17b-16e-instruct",
                messages = new object[] {
                    new { 
                        role = "system", 
                        content = "Eres un asistente médico experto. Tu objetivo es ofrecer diagnósticos diferenciales visuales e intuitivos.\n" +
                                  "REGLAS DE FORMATO:\n" +
                                  "1. Usa **negritas** para términos médicos importantes.\n" +
                                  "2. Usa ### Encabezados para secciones.\n" +
                                  "3. Usa listas con viñetas o numeradas para mayor claridad.\n" +
                                  "4. Sé directo, profesional y estructurado." 
                    },
                    new { 
                        role = "user", 
                        content = userContent
                    }
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("Authorization", $"Bearer {_groqApiKey}");
            request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseString);
            
            if (doc.RootElement.TryGetProperty("error", out var errorProp))
            {
                var msg = errorProp.TryGetProperty("message", out var m) ? m.GetString() : responseString;
                return $"Error de la IA: {msg}";
            }

            return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "Sin respuesta.";
        }
        catch (Exception ex)
        {
            return $"Error con Groq: {ex.Message}";
        }
    }
}
