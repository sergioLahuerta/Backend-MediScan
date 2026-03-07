using System;

namespace MediScan.Core.Entities;

public class Diagnosis
{
    public int Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid ProfessionalId { get; set; }
    public DateTime DiagnosisDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? ICD10Code { get; set; }

    public Patient Patient { get; set; } = null!;
    public Professional Professional { get; set; } = null!;
}
