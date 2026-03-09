using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface ISubscriptionService
{
    Task<IEnumerable<Subscription>> GetAllAsync();
    Task<Subscription?> GetByIdAsync(object id);
    Task<Subscription> AddAsync(Subscription entity);
    Task UpdateAsync(Subscription entity);
    Task DeleteAsync(Subscription entity);
}

