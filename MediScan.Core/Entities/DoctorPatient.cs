using System;

namespace MediScan.Core.Entities;

public class DoctorPatient
{
    public Guid PatientId { get; set; }
    public Guid ProfessionalId { get; set; }
    public DateTime CreatedAt { get; set; }

    public Patient Patient { get; set; } = null!;
    public Professional Professional { get; set; } = null!;
}
