using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IChatSessionService
{
    Task<IEnumerable<ChatSession>> GetAllAsync();
    Task<ChatSession?> GetByIdAsync(object id);
    Task<ChatSession> AddAsync(ChatSession entity);
    Task UpdateAsync(ChatSession entity);
    Task DeleteAsync(ChatSession entity);
}

