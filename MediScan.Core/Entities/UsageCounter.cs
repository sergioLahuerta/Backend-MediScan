using System;

namespace MediScan.Core.Entities;

public class UsageCounter
{
    public int Id { get; set; }
    public Guid? UserId { get; set; }
    public string? ClientIdentifier { get; set; }
    public int TokensUsed { get; set; }
    public int MessagesSent { get; set; }
    public DateTime? PeriodStart { get; set; }
    public DateTime? PeriodEnd { get; set; }

    public User? User { get; set; }
}
