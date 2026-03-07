using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using MediScan.Core.Interfaces.Services;
using MediScan.Api.Models;

namespace MediScan.Api.Controllers;

[ApiController]
[Microsoft.AspNetCore.Authorization.Authorize]
[Route("api/[controller]")]
public class ChatMessagesController : ControllerBase
{
    private readonly IChatMessageService _Service;

    public ChatMessagesController(IChatMessageService Service)
    {
        _Service = Service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _Service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(object id)
    {
        var result = await _Service.GetByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Envia un mensaje al modelo de IA y obtiene la respuesta. Guarda ambos mensajes en la
    /// tabla de chat antes de devolver la respuesta.
    /// </summary>
    [HttpPost("process")]
    public async Task<IActionResult> ProcessChat([FromBody] Models.ChatRequestDto request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Message))
            return BadRequest("El mensaje no puede estar vacío.");

        var aiText = await _Service.ProcessChatAsync(request.ChatSessionId, request.Message, request.Base64Image);
        return Ok(new { response = aiText });
    }
}


