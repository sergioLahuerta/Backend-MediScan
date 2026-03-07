using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface ITreatmentService
{
    Task<IEnumerable<Treatment>> GetAllAsync();
    Task<Treatment?> GetByIdAsync(object id);
    Task<Treatment> AddAsync(Treatment entity);
    Task UpdateAsync(Treatment entity);
    Task DeleteAsync(Treatment entity);
}

