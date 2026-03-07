using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Core.Interfaces.Services;

namespace MediScan.Application.Services;

public class ProfessionalService : IProfessionalService
{
    private readonly IProfessionalRepository _repository;

    public ProfessionalService(IProfessionalRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Professional>> GetAllAsync() => await _repository.GetAllAsync();
    public async Task<Professional?> GetByIdAsync(object id) => await _repository.GetByIdAsync(id);
    public async Task<Professional> AddAsync(Professional entity) => await _repository.AddAsync(entity);
    public async Task UpdateAsync(Professional entity) => await _repository.UpdateAsync(entity);
    public async Task DeleteAsync(Professional entity) => await _repository.DeleteAsync(entity);
}

