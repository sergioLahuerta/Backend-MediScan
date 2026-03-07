using System;
using MediScan.Core.Enums;

namespace MediScan.Core.Entities;

public class Subscription
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int PlanId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Active;

    public User User { get; set; } = null!;
    public Plan Plan { get; set; } = null!;
}
