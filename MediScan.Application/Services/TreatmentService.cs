using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class TreatmentService : ITreatmentService
{
    private readonly ITreatmentRepository _repository;

    public TreatmentService(ITreatmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Treatment>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Treatment?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<Treatment> AddAsync(Treatment entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Treatment entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(Treatment entity) => await _repository.DeleteAsync(entity);
}

