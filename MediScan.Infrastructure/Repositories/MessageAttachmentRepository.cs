using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Infrastructure.Data;

namespace MediScan.Infrastructure.Repositories;

public class MessageAttachmentRepository : Repository<MessageAttachment>, IMessageAttachmentRepository
{
    public MessageAttachmentRepository(MediScanDbContext context) : base(context) { }
}

