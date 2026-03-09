using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class MessageAttachmentService : IMessageAttachmentService
{
    private readonly IMessageAttachmentRepository _repository;

    public MessageAttachmentService(IMessageAttachmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MessageAttachment>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<MessageAttachment?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<MessageAttachment> AddAsync(MessageAttachment entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(MessageAttachment entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(MessageAttachment entity) => await _repository.DeleteAsync(entity);
}

