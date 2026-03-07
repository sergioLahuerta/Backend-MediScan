using System;
using System.Collections.Generic;

namespace MediScan.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public int? RoleId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public string? ProfileImageUrl { get; set; }

    // Navigation properties
    public Role? Role { get; set; }
    public Patient? Patient { get; set; }
    public Professional? Professional { get; set; }
    public ICollection<UsageCounter> UsageCounters { get; set; } = new List<UsageCounter>();
    public ICollection<ChatSession> ChatSessions { get; set; } = new List<ChatSession>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
