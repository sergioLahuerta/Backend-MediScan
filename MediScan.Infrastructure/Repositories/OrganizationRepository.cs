using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Infrastructure.Data;

namespace MediScan.Infrastructure.Repositories;

public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(MediScanDbContext context) : base(context) { }
}

