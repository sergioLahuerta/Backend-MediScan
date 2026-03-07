using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Infrastructure.Data;

namespace MediScan.Infrastructure.Repositories;

public class PlanRepository : Repository<Plan>, IPlanRepository
{
    public PlanRepository(MediScanDbContext context) : base(context) { }
}

