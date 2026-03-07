using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Infrastructure.Data;

namespace MediScan.Infrastructure.Repositories;

public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(MediScanDbContext context) : base(context) { }
}

