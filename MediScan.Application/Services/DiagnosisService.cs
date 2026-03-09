using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class DiagnosisService : IDiagnosisService
{
    private readonly IDiagnosisRepository _repository;

    public DiagnosisService(IDiagnosisRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Diagnosis>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Diagnosis?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<Diagnosis> AddAsync(Diagnosis entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Diagnosis entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(Diagnosis entity) => await _repository.DeleteAsync(entity);
}

