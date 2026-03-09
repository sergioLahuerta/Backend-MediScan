using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IPlanService
{
    Task<IEnumerable<Plan>> GetAllAsync();
    Task<Plan?> GetByIdAsync(object id);
    Task<Plan> AddAsync(Plan entity);
    Task UpdateAsync(Plan entity);
    Task DeleteAsync(Plan entity);
}

