using System;
using MediScan.Core.Enums;

namespace MediScan.Core.Entities;

public class Appointment
{
    public int Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid ProfessionalId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int DurationMinutes { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    public string? Notes { get; set; }

    // Navigation properties
    public Patient Patient { get; set; } = null!;
    public Professional Professional { get; set; } = null!;
}
