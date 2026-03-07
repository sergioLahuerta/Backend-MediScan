using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace MediScan.Application.Services;

public class ChatMessageService : IChatMessageService
{
    private readonly IChatMessageRepository _repository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _groqApiKey;

    public ChatMessageService(IChatMessageRepository repository, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _repository = repository;
        _httpClientFactory = httpClientFactory;
        _groqApiKey = configuration["Groq:ApiKey"] ?? string.Empty;
    }

    public async Task<IEnumerable<ChatMessage>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<ChatMessage?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<ChatMessage> AddAsync(ChatMessage entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(ChatMessage entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(ChatMessage entity) => await _repository.DeleteAsync(entity);

    /// <summary>
    /// Orquesta un intercambio de mensajes con el modelo de IA, guarda tanto la petición del usuario como
    /// la respuesta generada en la base de datos y devuelve el texto final.
    /// </summary>
    public async Task<string> ProcessChatAsync(Guid sessionId, string userMessage, string? base64Image = null)
    {
        // persistir mensaje del usuario
        await _repository.AddAsync(new ChatMessage
        {
            ChatSessionId = sessionId,
            Sender = "User",
            Content = userMessage,
            Timestamp = DateTime.UtcNow
        });

        string aiResponse = await GetAIResponse(userMessage, base64Image);

        // guardar respuesta de la IA
        await _repository.AddAsync(new ChatMessage
        {
            ChatSessionId = sessionId,
            Sender = "AI",
            Content = aiResponse,
            Timestamp = DateTime.UtcNow
        });

        return aiResponse;
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
                userContent.Add(new
                {
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
                        content = "Eres un asistente experto en soporte al diagnóstico médico clínico. Analiza la imagen técnica proporcionada y ofrece diagnósticos diferenciales."
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
            return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "Sin respuesta.";
        }
        catch (Exception ex)
        {
            return $"Error con IA: {ex.Message}";
        }
    }
}

