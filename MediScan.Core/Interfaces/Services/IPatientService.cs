using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IPatientService
{
    Task<IEnumerable<Patient>> GetAllAsync();
    Task<Patient?> GetByIdAsync(object id);
    Task<Patient> AddAsync(Patient entity);
    Task UpdateAsync(Patient entity);
    Task DeleteAsync(Patient entity);
}

