using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IDoctorPatientService
{
    Task<IEnumerable<DoctorPatient>> GetAllAsync();
    Task<DoctorPatient?> GetByIdAsync(object id);
    Task<DoctorPatient> AddAsync(DoctorPatient entity);
    Task UpdateAsync(DoctorPatient entity);
    Task DeleteAsync(DoctorPatient entity);
}

