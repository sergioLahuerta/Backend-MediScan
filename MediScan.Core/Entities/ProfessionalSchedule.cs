namespace MediScan.Core.Entities;

public class ProfessionalSchedule
{
    public int Id { get; set; }
    public Guid ProfessionalId { get; set; }
    public int DayOfWeek { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsAvailable { get; set; }

    public Professional Professional { get; set; } = null!;
}
