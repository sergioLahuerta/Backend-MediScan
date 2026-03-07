using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Infrastructure.Data;

namespace MediScan.Infrastructure.Repositories;

public class ProfessionalRepository : Repository<Professional>, IProfessionalRepository
{
    public ProfessionalRepository(MediScanDbContext context) : base(context)
    {
    }
}
