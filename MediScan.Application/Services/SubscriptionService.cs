using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly ISubscriptionRepository _repository;

    public SubscriptionService(ISubscriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Subscription>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Subscription?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<Subscription> AddAsync(Subscription entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Subscription entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(Subscription entity) => await _repository.DeleteAsync(entity);
}

