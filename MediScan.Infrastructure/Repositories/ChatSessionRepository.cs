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
            // 1. Get all messages of this session
            var messages = await _context.ChatMessages
                .Where(m => m.ChatSessionId == sessionGuid)
                .ToListAsync();

            if (messages.Any())
            {
                // 2. Get all attachments of those messages
                var messageIds = messages.Select(m => m.Id).ToList();
                var attachments = await _context.MessageAttachments
                    .Where(a => messageIds.Contains(a.ChatMessageId))
                    .ToListAsync();

                // 3. Remove attachments
                if (attachments.Any())
                {
                    _context.MessageAttachments.RemoveRange(attachments);
                }

                // 4. Remove messages
                _context.ChatMessages.RemoveRange(messages);
            }

            // 5. Save changes for relations first (or do all together)
            // But usually we can just remove everything and then SaveChanges once.
        }

        // 6. Delete the session itself using base logic (which calls SaveChanges)
        await base.DeleteAsync(id);
    }
}

