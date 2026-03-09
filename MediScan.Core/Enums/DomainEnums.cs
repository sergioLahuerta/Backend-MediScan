namespace MediScan.Core.Enums;

public enum SessionType
{
    Diagnosis,
    ProfessionalConsultation,
    General
}

public enum SenderType
{
    User,
    Guest,
    Professional,
    AI,
    System
}

public enum AppointmentStatus
{
    Scheduled,
    Completed,
    Cancelled,
    Rescheduled
}

public enum SubscriptionStatus
{
    Active,
    Expired,
    Cancelled,
    Pending
}

public enum PaymentStatus
{
    Pending,
    Completed,
    Failed,
    Refunded
}
