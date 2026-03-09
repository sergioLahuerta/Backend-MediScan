using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _repository;

    public PatientService(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Patient>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Patient?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<Patient> AddAsync(Patient entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Patient entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(Patient entity) => await _repository.DeleteAsync(entity);
}

