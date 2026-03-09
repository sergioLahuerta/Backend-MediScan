using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Infrastructure.Data;

namespace MediScan.Infrastructure.Repositories;

public class TreatmentRepository : Repository<Treatment>, ITreatmentRepository
{
    public TreatmentRepository(MediScanDbContext context) : base(context) { }
}

