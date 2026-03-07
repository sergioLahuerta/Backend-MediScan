using System;
using MediScan.Core.Enums;

namespace MediScan.Core.Entities;

public class ChatSession
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public SessionType SessionType { get; set; } = SessionType.Diagnosis;
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? EndedAt { get; set; }
    public string? Title { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
}
