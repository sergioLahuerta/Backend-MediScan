using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class ChatSessionService : IChatSessionService
{
    private readonly IChatSessionRepository _repository;

    public ChatSessionService(IChatSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ChatSession>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<ChatSession?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<ChatSession> AddAsync(ChatSession entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(ChatSession entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(ChatSession entity) => await _repository.DeleteAsync(entity);
}

