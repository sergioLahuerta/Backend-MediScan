using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class DoctorPatientService : IDoctorPatientService
{
    private readonly IDoctorPatientRepository _repository;

    public DoctorPatientService(IDoctorPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DoctorPatient>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<DoctorPatient?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<DoctorPatient> AddAsync(DoctorPatient entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(DoctorPatient entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(DoctorPatient entity) => await _repository.DeleteAsync(entity);
}

