using System;

namespace MediScan.Core.Entities;

public class Review
{
    public int Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid ProfessionalId { get; set; }
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }

    public Patient Patient { get; set; } = null!;
    public Professional Professional { get; set; } = null!;
}
