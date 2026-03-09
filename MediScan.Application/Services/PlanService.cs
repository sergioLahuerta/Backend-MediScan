using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class PlanService : IPlanService
{
    private readonly IPlanRepository _repository;

    public PlanService(IPlanRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Plan>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Plan?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<Plan> AddAsync(Plan entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Plan entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(Plan entity) => await _repository.DeleteAsync(entity);
}

