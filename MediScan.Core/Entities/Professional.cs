using System;
using System.Collections.Generic;

namespace MediScan.Core.Entities;

public class Professional
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Bio { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<DoctorPatient> DoctorPatients { get; set; } = new List<DoctorPatient>();
    public ICollection<ProfessionalOrganization> ProfessionalOrganizations { get; set; } = new List<ProfessionalOrganization>();
    public ICollection<ProfessionalSchedule> Schedules { get; set; } = new List<ProfessionalSchedule>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
