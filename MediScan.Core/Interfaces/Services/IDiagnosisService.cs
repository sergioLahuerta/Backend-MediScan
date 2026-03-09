using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IDiagnosisService
{
    Task<IEnumerable<Diagnosis>> GetAllAsync();
    Task<Diagnosis?> GetByIdAsync(object id);
    Task<Diagnosis> AddAsync(Diagnosis entity);
    Task UpdateAsync(Diagnosis entity);
    Task DeleteAsync(Diagnosis entity);
}

