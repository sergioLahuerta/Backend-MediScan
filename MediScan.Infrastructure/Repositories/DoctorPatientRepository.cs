using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Infrastructure.Data;

namespace MediScan.Infrastructure.Repositories;

public class DoctorPatientRepository : Repository<DoctorPatient>, IDoctorPatientRepository
{
    public DoctorPatientRepository(MediScanDbContext context) : base(context) { }
}

