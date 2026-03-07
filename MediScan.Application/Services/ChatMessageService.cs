using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class ChatMessageService : IChatMessageService
{
    private readonly IChatMessageRepository _repository;

    public ChatMessageService(IChatMessageRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ChatMessage>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<ChatMessage?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<ChatMessage> AddAsync(ChatMessage entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(ChatMessage entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(ChatMessage entity) => await _repository.DeleteAsync(entity);
}

