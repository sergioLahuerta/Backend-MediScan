using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Repositories;

public interface IChatSessionRepository : IRepository<ChatSession>
{
    Task<List<ChatSession>> GetSessionsByUserIdAsync(Guid userId);
}

