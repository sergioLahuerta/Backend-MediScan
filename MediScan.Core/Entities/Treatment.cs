namespace MediScan.Core.Entities;

public class Treatment
{
    public int Id { get; set; }
    public int DiagnosisId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Diagnosis Diagnosis { get; set; } = null!;
}
