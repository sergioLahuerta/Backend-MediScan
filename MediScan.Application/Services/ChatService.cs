using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class ChatService : IChatService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _groqApiKey;

    public ChatService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        
        // Sacamos la key de appsettings.json, si no está cogemos la harcodeada del temp (de fallback)
        _groqApiKey = configuration["Groq:ApiKey"] 
                     ?? "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";
    }

    public async Task<string> ProcessChatAsync(string sessionId, string userMessage, string? base64Image)
    {
        // En temp, esto llamaba a _repository.AddMessageAsync(...) para la request y response.
        // Dado el 500 error anterior, no usamos DB SQL directamente aquí para evitar bloqueos si no está configurada,
        // igual que vimos, nos centraremos en devolver el AI response correctamente.
        return await GetAIResponse(userMessage, base64Image);
    }

    private async Task<string> GetAIResponse(string prompt, string? base64Image = null)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();
            var url = "https://api.groq.com/openai/v1/chat/completions";

            // 1. Preparamos el contenido del usuario (Texto + Imagen)
            var userContent = new List<object> { new { type = "text", text = prompt } };

            if (!string.IsNullOrEmpty(base64Image))
            {
                userContent.Add(new { 
                    type = "image_url", 
                    image_url = new { url = $"data:image/jpeg;base64,{base64Image}" } 
                });
            }

            // 2. Construimos la lista de mensajes con el modelo LLAMA 4 SCOUT (Igual que en temp)
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
            
            // Atrapamos errores de la API como cuota excedida
            if (doc.RootElement.TryGetProperty("error", out var errorProp))
            {
                var msg = errorProp.TryGetProperty("message", out var m) ? m.GetString() : responseString;
                return $"Error de la IA: {msg}";
            }

            return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "Sin respuesta.";
        }
        catch (Exception ex)
        {
            return $"Error con Llama 4: {ex.Message}";
        }
    }
}
