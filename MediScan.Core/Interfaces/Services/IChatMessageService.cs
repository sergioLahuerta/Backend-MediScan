using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IChatMessageService
{
    Task<IEnumerable<ChatMessage>> GetAllAsync();
    Task<ChatMessage?> GetByIdAsync(object id);
    Task<ChatMessage> AddAsync(ChatMessage entity);
    Task UpdateAsync(ChatMessage entity);
    Task DeleteAsync(ChatMessage entity);

    // new method to process a conversation with the AI and persist both user and AI messages
    Task<string> ProcessChatAsync(Guid sessionId, string userMessage, string? base64Image = null);
}

