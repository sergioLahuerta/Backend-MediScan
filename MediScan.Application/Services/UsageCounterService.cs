using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class UsageCounterService : IUsageCounterService
{
    private readonly IUsageCounterRepository _repository;

    public UsageCounterService(IUsageCounterRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UsageCounter>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<UsageCounter?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<UsageCounter> AddAsync(UsageCounter entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(UsageCounter entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(UsageCounter entity) => await _repository.DeleteAsync(entity);
}

