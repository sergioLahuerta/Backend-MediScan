using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IMessageAttachmentService
{
    Task<IEnumerable<MessageAttachment>> GetAllAsync();
    Task<MessageAttachment?> GetByIdAsync(object id);
    Task<MessageAttachment> AddAsync(MessageAttachment entity);
    Task UpdateAsync(MessageAttachment entity);
    Task DeleteAsync(MessageAttachment entity);
}

