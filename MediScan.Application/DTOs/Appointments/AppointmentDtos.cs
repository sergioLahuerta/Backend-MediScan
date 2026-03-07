using System;
using MediScan.Core.Enums;

namespace MediScan.Application.DTOs.Appointments;

public class AppointmentDto
{
    public int Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid ProfessionalId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int DurationMinutes { get; set; }
    public AppointmentStatus Status { get; set; }
    public string? Notes { get; set; }
}
