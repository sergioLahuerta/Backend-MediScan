using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Repositories;

public interface IChatMessageRepository : IRepository<ChatMessage>
{
    Task<List<ChatMessage>> GetMessagesBySessionIdAsync(Guid sessionId);
}

