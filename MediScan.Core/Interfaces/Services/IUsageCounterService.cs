using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IUsageCounterService
{
    Task<IEnumerable<UsageCounter>> GetAllAsync();
    Task<UsageCounter?> GetByIdAsync(object id);
    Task<UsageCounter> AddAsync(UsageCounter entity);
    Task UpdateAsync(UsageCounter entity);
    Task DeleteAsync(UsageCounter entity);
}

