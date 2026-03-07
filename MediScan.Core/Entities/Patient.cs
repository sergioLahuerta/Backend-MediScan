using System;
using System.Collections.Generic;

namespace MediScan.Core.Entities;

public class Patient
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Phone { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<DoctorPatient> DoctorPatients { get; set; } = new List<DoctorPatient>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
