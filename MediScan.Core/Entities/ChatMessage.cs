using MediScan.Core.Enums;

namespace MediScan.Core.Entities;

public class ChatMessage
{
    public long Id { get; set; }
    public Guid ChatSessionId { get; set; }
    public SenderType SenderType { get; set; } = SenderType.User;
    public string MessageText { get; set; } = string.Empty;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public ChatSession ChatSession { get; set; } = null!;
    public ICollection<MessageAttachment> Attachments { get; set; } = new List<MessageAttachment>();
}
