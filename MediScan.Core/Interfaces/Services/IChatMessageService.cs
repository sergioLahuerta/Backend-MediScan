using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IChatMessageService
{
    Task<IEnumerable<ChatMessage>> GetAllAsync();
    Task<ChatMessage?> GetByIdAsync(object id);
    Task<ChatMessage> AddAsync(ChatMessage entity);
    Task UpdateAsync(ChatMessage entity);
    Task DeleteAsync(ChatMessage entity);
}

