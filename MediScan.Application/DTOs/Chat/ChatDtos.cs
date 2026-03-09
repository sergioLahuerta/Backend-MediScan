using MediScan.Core.Enums;

namespace MediScan.Application.DTOs.Chat;

public class ChatSessionDto
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public SessionType SessionType { get; set; }
    public string? Title { get; set; }
}

public class ChatMessageDto
{
    public long Id { get; set; }
    public SenderType SenderType { get; set; }
    public string MessageText { get; set; } = string.Empty;
    public DateTime SentAt { get; set; }
}
