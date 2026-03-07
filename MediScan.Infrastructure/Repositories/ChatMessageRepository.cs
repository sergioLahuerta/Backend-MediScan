using Microsoft.EntityFrameworkCore;
using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Infrastructure.Data;

namespace MediScan.Infrastructure.Repositories;

public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(MediScanDbContext context) : base(context) { }

    public async Task<List<ChatMessage>> GetMessagesBySessionIdAsync(Guid sessionId)
    {
        return await _dbSet
            .Where(m => m.ChatSessionId == sessionId)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }
}

