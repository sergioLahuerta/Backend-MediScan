using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Infrastructure.Data;

namespace MediScan.Infrastructure.Repositories;

public class ProfessionalScheduleRepository : Repository<ProfessionalSchedule>, IProfessionalScheduleRepository
{
    public ProfessionalScheduleRepository(MediScanDbContext context) : base(context) { }
}

