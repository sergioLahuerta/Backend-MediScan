namespace MediScan.Core.Entities;

public class ProfessionalOrganization
{
    public Guid ProfessionalId { get; set; }
    public Guid OrganizationId { get; set; }
    public DateTime JoinedAt { get; set; }

    public Professional Professional { get; set; } = null!;
    public Organization Organization { get; set; } = null!;
}
