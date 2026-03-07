using System;
using MediScan.Core.Enums;

namespace MediScan.Core.Entities;

public class Payment
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? PaymentMethod { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

    public Subscription Subscription { get; set; } = null!;
}
