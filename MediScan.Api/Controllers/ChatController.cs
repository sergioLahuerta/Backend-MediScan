using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using System;
using MediScan.Core.Interfaces.Services;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Entities;
using MediScan.Core.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace MediScan.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Microsoft.AspNetCore.Authorization.AllowAnonymous]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IChatSessionRepository _chatSessionRepository;
    private readonly IMemoryCache _cache;
    private const int GuestDailyLimit = 3;

    public ChatController(IChatService chatService, IChatSessionRepository chatSessionRepository, IMemoryCache cache)
    {
        _chatService = chatService;
        _chatSessionRepository = chatSessionRepository;
        _cache = cache; // Para seguir manteniendo el requerimiento de "solo 3 sin login" del usuario
    }

    [HttpPost("session")]
    public async Task<IActionResult> StartSession()
    {
        // En temp tomaban {userId} asume login, pero aquí pidieron "funciona sin login" 
        // Generamos un Guid aleatorio y lo devolvemos para que el chat funcione.
        var sessionId = Guid.NewGuid();
        
        try 
        {
            // Intentamos persistir en DB si está disponible
            await _chatSessionRepository.AddAsync(new ChatSession
            {
                Id = sessionId,
                SessionType = SessionType.Diagnosis,
                StartedAt = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            // Si la DB falla (ej: MySQL apagado), logueamos pero permitimos que el chat siga en memoria
            Console.WriteLine($"[DATABASE ERROR - Session]: {ex.Message}");
        }

        return Ok(new { sessionId = sessionId.ToString() });
    }

    // 2. Enviar un mensaje y recibir respuesta de la IA
    [HttpPost("{sessionId}/send")]
    public async Task<IActionResult> SendMessage(string sessionId, [FromForm] string? message, IFormFile? file)
    {
        // ── Límite de 3 para usuarios no logueados ───────────────────────
        bool isAuthenticated = User.Identity?.IsAuthenticated ?? false;
        if (!isAuthenticated)
        {
            string ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            string cacheKey = $"groq_guest_{ip}_{DateTime.UtcNow:yyyyMMdd}";

            int count = _cache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpiration = DateTime.UtcNow.Date.AddDays(1);
                return 0;
            });

            if (count >= GuestDailyLimit)
            {
                return StatusCode(429, new
                {
                    error = "guest_limit_reached",
                    message = $"Has alcanzado el límite de {GuestDailyLimit} mensajes gratuitos al día. Crea una cuenta para disfrutar sin límites."
                });
            }

            _cache.Set(cacheKey, count + 1, DateTime.UtcNow.Date.AddDays(1));
        }
        // ──────────────────────────────────────────────────────────────────

        // Extraer la imagen base64 igual que en temp
        string? base64Image = null;

        if (file != null && file.Length > 0)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            base64Image = Convert.ToBase64String(ms.ToArray());
        }

        // Usamos un texto por defecto si el mensaje está vacío
        string finalMessage = string.IsNullOrWhiteSpace(message) ? "Analiza el contenido de esta imagen clínica." : message;

        // PASAMOS LOS 3 ARGUMENTOS (Importante: revisa que tu Service los reciba), esto es igual que en temp
        var response = await _chatService.ProcessChatAsync(sessionId, finalMessage, base64Image);

        return Ok(new { userMessage = finalMessage, aiResponse = response });
    }
}
