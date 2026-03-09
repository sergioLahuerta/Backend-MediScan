using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Infrastructure.Data;

namespace MediScan.Infrastructure.Repositories;

public class ProfessionalOrganizationRepository : Repository<ProfessionalOrganization>, IProfessionalOrganizationRepository
{
    public ProfessionalOrganizationRepository(MediScanDbContext context) : base(context) { }
}

