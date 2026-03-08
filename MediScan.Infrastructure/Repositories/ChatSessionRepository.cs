using Microsoft.EntityFrameworkCore;
using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Infrastructure.Data;

namespace MediScan.Infrastructure.Repositories;

public class ChatSessionRepository : Repository<ChatSession>, IChatSessionRepository
{
    public ChatSessionRepository(MediScanDbContext context) : base(context) { }

    public async Task<List<ChatSession>> GetSessionsByUserIdAsync(Guid userId)
    {
        return await _dbSet
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.StartedAt)
            .ToListAsync();
    }
    public override async Task DeleteAsync(object id)
    {
        if (id is Guid sessionGuid)
        {
            // Coger todos los mensajes de la sesión
            var messages = await _context.ChatMessages
                .Where(m => m.ChatSessionId == sessionGuid)
                .ToListAsync();

            if (messages.Any())
            {
                // Coger los adjuntos de esa sesión
                var messageIds = messages.Select(m => m.Id).ToList();
                var attachments = await _context.MessageAttachments
                    .Where(a => messageIds.Contains(a.ChatMessageId))
                    .ToListAsync();

                if (attachments.Any())
                {
                    _context.MessageAttachments.RemoveRange(attachments);
                }
                _context.ChatMessages.RemoveRange(messages);
            }
        }

        // Eliminar la sesión
        await base.DeleteAsync(id);
    }
}

