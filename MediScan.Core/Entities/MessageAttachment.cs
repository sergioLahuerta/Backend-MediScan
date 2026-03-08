namespace MediScan.Core.Entities;

public class MessageAttachment
{
    public long Id { get; set; }
    public long ChatMessageId { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; }

    public ChatMessage ChatMessage { get; set; } = null!;
}
