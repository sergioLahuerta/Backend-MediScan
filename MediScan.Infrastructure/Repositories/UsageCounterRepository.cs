using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Infrastructure.Data;

namespace MediScan.Infrastructure.Repositories;

public class UsageCounterRepository : Repository<UsageCounter>, IUsageCounterRepository
{
    public UsageCounterRepository(MediScanDbContext context) : base(context) { }
}

